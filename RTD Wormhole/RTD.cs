using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace RTD_Wormhole
{
    [ComImport,
TypeLibType((short)0x1040),
Guid("EC0E6191-DB51-11D3-8F3E-00C04F3651B8")]
    public interface IRtdServer
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(10)]
        int ServerStart([In, MarshalAs(UnmanagedType.Interface)] IRTDUpdateEvent callback);

        [return: MarshalAs(UnmanagedType.Struct)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(11)]
        object ConnectData([In] int topicId, [In, MarshalAs(UnmanagedType.SafeArray,
                                                    SafeArraySubType = VarEnum.VT_VARIANT)] ref object[] parameters, [In, Out] ref bool newValue);

        [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(12)]
        object[,] RefreshData([In, Out] ref int topicCount);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(13)]
        void DisconnectData([In] int topicId);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(14)]
        int Heartbeat();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(15)]
        void ServerTerminate();
    }

    [ComImport,
        TypeLibType((short)0x1040),
        Guid("A43788C1-D91B-11D3-8F39-00C04F3651B8")]

    public interface IRTDUpdateEvent
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(10),
            PreserveSig]
        void UpdateNotify();

        [DispId(11)]
        int HeartbeatInterval
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(11)]
            get;
            [param: In]
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(11)]
            set;
        }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), DispId(12)]
        void Disconnect();
    }

    public class UpdateEvent : IRTDUpdateEvent
    {
        public int HeartbeatInterval { get; set; }
        private readonly Action _eventHandler;

        public UpdateEvent(Action eventHandler, int refreshRate)
        {
            HeartbeatInterval = refreshRate;
            _eventHandler = eventHandler;
        }

        public void Disconnect()
        {
            // do nothing, not clear if this is a handler...
        }

        public void UpdateNotify()
        {
            _eventHandler();
        }
    }

    public interface IRtdClient
    {
        event EventHandler EConnect;
        event EventHandler EDisconnect;
        event EventHandler<DataEventArgs> Data;
        bool Connected
        {
            get;
        }
        bool Connect(string progID, int refreshRate);
        void Disconnect();
        void Subscribe(int topicID, object[] topicParams);
        void Unsubscribe(int topicID);
    }

    public class DataEventArgs : EventArgs
    {
        public object[,] Data { get; set; }
        public int Count { get; set; }

        public DataEventArgs(int count, object[,] data)
        {
            this.Data = data;
            this.Count = count;
        }
    }

    public class RtdClient : IRtdClient
    {
        private Timer timer;
        private bool connected;
        private IRtdServer rtdServer;

        public event EventHandler EConnect;
        public event EventHandler EDisconnect;
        public event EventHandler HeartBeatLost;
        public event EventHandler<DataEventArgs> Data;


        protected virtual void OnConnect()
        {
            EConnect?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnHeartBeatLost()
        {
            HeartBeatLost?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDisconnect()
        {
            EDisconnect?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnData(DataEventArgs data)
        {
            Data?.Invoke(this, data);
        }

        /// <summary>
        /// process a newData event
        /// </summary>
        private void NewDataEvent()
        {
            // receive data from server
            int topicCount = 0;
            object[,] data = rtdServer.RefreshData(ref topicCount);
            if (topicCount > 0)
            {
                // pass data to handler
                OnData(new DataEventArgs(topicCount, data));
            }
        }

        /// <summary>
        /// indicates if connected
        /// </summary>
        public bool Connected
        {
            get
            {
                return connected;
            }
        }

        /// <summary>
        /// connect to a RTD Server
        /// </summary>
        /// <param name="progID">RTD Server ProgID</param>
        /// <param name="refreshRate">data refresh rate, default = -1</param>
        /// <returns>true if connected successfully, false otherwise</returns>
        public bool Connect(string progID, int refreshRate)
        {
            if (!connected)
            {
                // create RTD server instance
                if (rtdServer == null)
                {
                    Type rtd = Type.GetTypeFromProgID(progID);
                    if (rtd == null)
                    {
                        return false;
                    }
                    rtdServer = (IRtdServer)Activator.CreateInstance(rtd);
                }

                // create updateEvent
                UpdateEvent updateEvent = new UpdateEvent(NewDataEvent, refreshRate);

                // connect
                object ret = rtdServer.ServerStart(updateEvent);
                if (ret != null)
                {
                    if ((int)ret >= 0)
                    {
                        connected = true;
                        OnConnect();
                        // establish heartbeat
                        timer = new Timer(new TimerCallback(TimerEventHandler), null, 0, 5000);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void TimerEventHandler(object state)
        {
            if (rtdServer.Heartbeat() != 1)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                rtdServer.ServerTerminate();
                connected = false;
                OnHeartBeatLost();
            }
        }

        public void Disconnect()
        {
            if (connected)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                rtdServer.ServerTerminate();
                connected = false;
                OnDisconnect();
            }
        }

        public void Subscribe(int topicID, object[] topicParams)
        {
            if (connected)
            {
                rtdServer.ConnectData(topicID, topicParams, true);
            }
        }

        public void Unsubscribe(int topicID)
        {
            if (connected)
            {
                rtdServer.DisconnectData(topicID);
            }
        }
    }
}