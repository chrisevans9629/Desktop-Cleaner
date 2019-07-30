using System;
using System.IO;

namespace DesktopCleaner
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            CleanDesktop();
        }

        private static void CleanDesktop()
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var desktopdirectory = desktop;
            var desktopitems = Directory.GetFiles(desktopdirectory);
            foreach (var item in desktopitems)
            {
                var exts = item.Split('.');
                if (exts.Length > 1)
                {
                    var ext = exts[exts.Length - 1];
                    if (ext != "lnk")
                    {
                        if (ext == "jpg" || ext == "png")
                        {
                            MoveToNewDirectory(desktopdirectory, item, "photos");
                        }
                        else if (ext == "docx" || ext == "txt" || ext == "log" || ext == "rtf" || ext == "pdf")
                        {
                            MoveToNewDirectory(desktopdirectory, item, "doc");
                        }
                        else if (ext == "cmd" || ext == "ps1" || ext == "bat")
                        {
                            MoveToNewDirectory(desktopdirectory, item, "script");
                        }
                        else if (ext == "csv" || ext == "xls" || ext == "xlsx")
                        {
                            MoveToNewDirectory(desktopdirectory, item, "excel");
                        }
                        else if (ext == "exe" || ext == "msi")
                        {
                            MoveToNewDirectory(desktopdirectory, item, "installer");
                        }
                        else
                        {
                            MoveToNewDirectory(desktopdirectory, item, ext);
                        }
                    }
                }
            }

            Console.WriteLine("Files Moved!");
            Console.ReadLine();
        }

        static void MoveToNewDirectory(string desktopdirectory, string item, string ext)
        {
            var directory = desktopdirectory + "\\" + ext + "Files";
            if (Directory.Exists(directory) != true)
            {
                Directory.CreateDirectory(directory);
            }
            var names = item.Split('\\');
            var name = names[names.Length - 1];
            var newfiledirectory = directory + "\\" + name;
            var test = CheckForExistingAndChange(newfiledirectory);
            if (test != null)
            {
                newfiledirectory = test;
            }
            File.Move(item, newfiledirectory);
            Console.WriteLine(item + " has been moved to " + directory);
        }
        static string CheckForExistingAndChange(string file)
        {
            if (File.Exists(file))
            {
                FileInfo deskfile = new FileInfo(file);

                var newname = deskfile.Directory.FullName + "\\" + Path.GetFileNameWithoutExtension(file) + 1 + deskfile.Extension;
                var testname = CheckForExistingAndChange(newname);
                if (testname == null)
                {
                    return newname;


                }
                return testname;
            }
            return null;
        }
    }
}
