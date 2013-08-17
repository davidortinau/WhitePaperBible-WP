using System.Windows;
using System.Windows.Media;
using System.Text;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace WhitePaperBible.Mobile.Helpers
{
    public static class WebBrowserHelper
    {
        public static string NotifyScript
        {
            get
            {
                return @"<script>
                    window.onload = function(){
                        a = document.getElementsByTagName('a');
                        for(var i=0; i < a.length; i++){
                            var msg = a[i].href;
                            a[i].onclick = function() {notify(msg);};
                        }
                    }
                    function notify(msg) {
	                window.external.Notify(msg); 
	                event.returnValue=false;
	                return false;
                    }
                    </script>";
            }
        }

        public static string WrapHtml(string htmlSubString, double viewportWidth)
        {
            var html = new StringBuilder();
            html.Append("<html>");
            html.Append(HtmlHeader(viewportWidth));
            html.Append("<body>");
            html.Append(htmlSubString);
            html.Append("</body>");
            html.Append("</html>");
            return html.ToString();
        }

        public static string HtmlHeader(double viewportWidth)
        {
            var head = new StringBuilder();

            head.Append("<head>");
            head.Append(string.Format(
                "<meta name=\"viewport\" value=\"width={0}\" user-scalable=\"no\" />",
                viewportWidth));
            head.Append("<style>");
            head.Append("html { -ms-text-size-adjust:150% }");
            head.Append(string.Format(
                "body {{background:{0};color:{1};font-family:'Segoe WP';font-size:{2}pt;margin:0;padding:0 }}",
                GetBrowserColor("PhoneBackgroundColor"),
                GetBrowserColor("PhoneForegroundColor"),
                (double)Application.Current.Resources["PhoneFontSizeLarge"]));
            head.Append(string.Format(
                "a {{color:{0}}}",
                GetBrowserColor("PhoneAccentColor")));
            head.Append("</style>");
            head.Append(NotifyScript);
            head.Append("</head>");


            return head.ToString();
        }


        public static void OpenBrowser(string url)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask { URL = url };
            webBrowserTask.Show();
        }

        private static string GetBrowserColor(string sourceResource)
        {
            var color = (Color)Application.Current.Resources[sourceResource];

            return "#" + color.ToString().Substring(3, 6);
        }

        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
        "Html", typeof(string), typeof(WebBrowserHelper), new PropertyMetadata(OnHtmlChanged));

        public static string GetHtml(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browser = d as WebBrowser;

            if (browser == null)
                return;
            
            var html = e.NewValue.ToString();

            browser.NavigateToString(html);
        }
    }
}
