namespace Summary.WASenderApi
{
    using Azure;
    using Core.Workflows;
    using System;
    public class AccountDoesNotExist : Exception { }

    public static class ThrowExceptionIf
    {
        public static void ResponseIsNotValid(string message, string data)
        {
            ResponseIsNotValid(message, data, string.Empty);
        }

        public static void ResponseIsNotValid(string message, string data, string to)
        {
            ThrowExceptionIf.JidDoesNotExist(message);
            ThrowExceptionIf.InvalidWhatsappJid(message, data, to);
            ThrowExceptionIf.SessionIsNotConnected(message, data);
            ThrowExceptionIf.SubmitToNextLevel(message, data);
        }

        public static void TokenIsEmpty(string token)
        {
            if (String.IsNullOrWhiteSpace(token) is false) return;

            throw new WorkflowException(
                "مقدار فیلد توکن در تنظیمات افزونه خالی رها شده است. مقداردهی این فیلد الزامی است.",
                null,
                null,
                "برای مرتفع نمودن این باگ کافیست در تنظیمات پلتفرم سامری فیلد «توکن» را با مقدار صحیح مقداردهی نمایید. در صورتیکه به توکن دسترسی ندارید با ثبت تیکت از اپراتورهای پشتیبانی درخواست کنید توکن را در اختیار شما قرار دهند یا تنظیمات افزونه را بروزرسانی کنند. با تشکر از همراهی شما."
            );
        }

        public static void SessionIsNotConnected(string message, string data)
        {
            if (message.ToLower().Contains("whatsapp session is not connected")) throw new WorkflowException(
                message,
                null,
                data,
                "کارشناس پشتیبانی؛ حساب واتساپ از سیستم خارج (Logout) شده است. لطفاً از طریق ثبت نظر، موضوع را به رابط مربوطه اطلاع دهید تا جهت اتصال مجدد، کد QR را اسکن نمایند."
            );
        }

        public static void InvalidWhatsappJid(string message, string data, string to)
        {
            if (message.ToLower().Contains("must be a valid WhatsApp jid")) throw new WorkflowException(
                message,
                null,
                data,
                $"رابط گرامی؛ خطاء فوق زمانی رخ می‌دهد که شماره واتساپ({to}) وارد شده نامعتبر باشد. خواهشمند است نسبت به بررسی و اصلاح شماره اقدام فرمایید. با سپاس از همراهی شما."
            );
        }

        public static void SubmitToNextLevel(string message, string data)
        {
            throw new WorkflowException(
                message,
                null,
                data,
                "کارشناس پشتیبانی؛ این تیکت را به سطح بعدی ارجاع دهید. با تشکر"
            );
        }

        public static void JidDoesNotExist(string message)
        {
            if (message.ToLower().Contains("jid does not exist on whatsapp"))
                throw new AccountDoesNotExist();
        }
    }
}