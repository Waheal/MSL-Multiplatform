using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System;

public class MessageBoxHelper
{
    public static void ShowMessageBox(Window parent, string message, string title="", string header = "",ButtonEnum buttonEnum=ButtonEnum.Ok)
    {
        var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
        if (assets != null)
        {
            var icon = new WindowIcon(assets.Open(new Uri("avares://MSL-Multiplatform/Assets/icon.ico")));

            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
            {
                ButtonDefinitions = buttonEnum,
                ContentTitle = title,
                ContentHeader = header,
                ContentMessage = message,
                WindowIcon = icon,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            });
            messageBoxStandardWindow.ShowDialog(parent);
        }
    }
}