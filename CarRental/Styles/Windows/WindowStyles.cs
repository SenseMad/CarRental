using System;
using System.Windows;
using System.Windows.Input;

namespace CarRental.Styles.Windows
{
  internal static class LocalExtensions
  {
    public static void ForWindowFromTemplate(this object parTemplateFrameworkElement, Action<Window> parAction)
    {
      if (((FrameworkElement)parTemplateFrameworkElement).TemplatedParent is Window window) parAction(window);
    }
  }

  public partial class WindowStyles
  {
    private void OnButtonCloseClick(object parSender, RoutedEventArgs parE)
    {
      parSender.ForWindowFromTemplate(parW => parW.Close());
      Application.Current.Shutdown();
    }
    private void OnButtonMinClick(object parSender, RoutedEventArgs parE)
    {
      parSender.ForWindowFromTemplate(parW => parW.WindowState = WindowState.Minimized);
    }
  }
}