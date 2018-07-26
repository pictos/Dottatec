using Dottatec.Interfaces;
using Dottatec.UWP.Dependencia;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificacaoUWP))]

namespace Dottatec.UWP.Dependencia
{
    public class NotificacaoUWP : INotificacao
    {
        public void Notificar(string msg)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(msg));

            //XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/Logo.scale-240.png");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "logo");

            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("duration", "short");

            var toastNavigationUriString = "#/MainPage.xaml?param1=12345";
            var toastElement = ((XmlElement)toastXml.SelectSingleNode("/toast"));
            toastElement.SetAttribute("launch", toastNavigationUriString);

            ToastNotification toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
