using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MultiLauncher.UI
{
    public class ProgramLocatorService
    {
        public IEnumerable<(string name, string path)> AutoLocatePrograms()
        {
            var registry_key_32 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            var registry_key_64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

            var programs = new List<(string name, string path)>();

            // Local Machine
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(Environment.Is64BitOperatingSystem ? registry_key_64 : registry_key_32))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        var name = subkey.GetValue("DisplayName");
                        var path = subkey.GetValue("InstallLocation");

                        if (name is null)
                            continue;

                        if (string.IsNullOrWhiteSpace(path?.ToString()))
                            continue;

                        programs.Add((name.ToString(), path.ToString()));
                    }
                }
            }

            // Current User
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registry_key_32))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        var name = subkey.GetValue("DisplayName");
                        var path = subkey.GetValue("InstallLocation");

                        if (name is null)
                            continue;

                        if (string.IsNullOrWhiteSpace(path?.ToString()))
                            continue;

                        programs.Add((name.ToString(), path.ToString()));
                    }
                }
            }

            return programs;
        }


        public ImageSource IconFromFilePath(string filePath)
        {
            try
            {
                var icon = Icon.ExtractAssociatedIcon(filePath);

                ImageSource img = Imaging.CreateBitmapSourceFromHIcon(
                                    icon.Handle,
                                    new Int32Rect(0, 0, icon.Width, icon.Height),
                                    BitmapSizeOptions.FromEmptyOptions());

                return img;
            }
            catch (Exception)
            {
                // swallow and return nothing. You could supply a default Icon here as well
            }

            return null;
        }
    }
}