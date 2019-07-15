using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Toast;

namespace TravelApp.Helpers
{
    static class Toast
    {
        public static void Success(string message)
        {
            CrossToastPopUp.Current.ShowCustomToast(message, "#449D44", "#FFFFFF");
        }

        public static void Error(string message ="เกิดข้อผิดพลาด")
        {
            CrossToastPopUp.Current.ShowCustomToast(message, "#C9302C", "#FFFFFF");
        }

        public static void Warning(string message)
        {
            CrossToastPopUp.Current.ShowCustomToast(message, "#EC971F", "#FFFFFF");
        }

        public static void Show(string message = "กรุณากรอกข้อมูล")
        {
            CrossToastPopUp.Current.ShowCustomToast(message, "#000000", "#FFFFFF");
        }
    }
}
