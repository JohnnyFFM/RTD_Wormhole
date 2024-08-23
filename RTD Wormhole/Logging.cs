using System;
using System.Windows.Forms;
using NLog;
using NLog.Targets;
using System.Drawing;

namespace RTD_Wormhole
{
    [Target("RichTextBoxTarget")]
    public class RichTextBoxTarget : TargetWithLayout
    {
        public RichTextBox RichTextBox { get; set; }

        protected override void Write(LogEventInfo logEvent)
        {
            if (RichTextBox != null)
            {
                if (RichTextBox.InvokeRequired)
                {
                    RichTextBox.Invoke(new Action(() =>
                    {
                        AppendTextToRichTextBox(logEvent);
                    }));
                }
                else
                {
                    AppendTextToRichTextBox(logEvent);
                }
            }
        }

        private void AppendTextToRichTextBox(LogEventInfo logEvent)
        {
            var message = Layout.Render(logEvent);
            var color = GetColorForLevel(logEvent.Level);

            RichTextBox.SelectionStart = RichTextBox.TextLength;
            RichTextBox.SelectionLength = 0;
            RichTextBox.SelectionColor = color;
            RichTextBox.AppendText(message + Environment.NewLine);
            RichTextBox.SelectionColor = RichTextBox.ForeColor; // Reset to default color
            RichTextBox.ScrollToCaret();
        }

        private Color GetColorForLevel(LogLevel level)
        {
            if (level == LogLevel.Info) return Color.Black; // Info in Schwarz
            if (level == LogLevel.Warn) return Color.DarkGoldenrod; // Warnung in Dunkelgold
            if (level == LogLevel.Error) return Color.Firebrick; // Fehler in Feuerrot
            if (level == LogLevel.Fatal) return Color.DarkRed; // Kritisch in Dunkelrot
            return Color.Black; // Default color
        }
    }

}