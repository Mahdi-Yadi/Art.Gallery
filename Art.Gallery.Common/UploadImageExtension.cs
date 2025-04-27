using Microsoft.AspNetCore.Http;
namespace Art.Gallery.Common;
public static class UploadImageExtension
{
    public static bool AddImageToServer(this IFormFile image, string fileName, string orginalPath, int? width, int? height, string thumbPath = null, string deletefileName = null)
    {
        if (image != null && image.IsImage())
        {
            // ایجاد دایرکتوری در صورت عدم وجود
            if (!Directory.Exists(orginalPath))
                Directory.CreateDirectory(orginalPath);

            // حذف فایل قدیمی در صورت وجود
            if (!string.IsNullOrEmpty(deletefileName))
            {
                if (File.Exists(orginalPath + deletefileName))
                    File.Delete(orginalPath + deletefileName);

                if (!string.IsNullOrEmpty(thumbPath) && File.Exists(thumbPath + deletefileName))
                {
                    File.Delete(thumbPath + deletefileName);
                }
            }

            // مسیر فایل اصلی
            string originPath = orginalPath + fileName;

            // ذخیره تصویر در مسیر اصلی
            using (var stream = new FileStream(originPath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            // بررسی اینکه آیا اندازه‌ها مشخص شده‌اند
            if (width != null && height != null)
            {
                ImageOptimizer resizer = new ImageOptimizer();

                // تغییر اندازه تصویر اصلی در صورت وارد کردن اندازه‌ها
                resizer.ImageResizer(originPath, originPath, width, height);
            }

            // بررسی مسیر Thumbnail در صورت وارد شدن
            if (!string.IsNullOrEmpty(thumbPath))
            {
                if (!Directory.Exists(thumbPath))
                    Directory.CreateDirectory(thumbPath);

                ImageOptimizer resizer = new ImageOptimizer();

                // ایجاد تصویر Thumbnail
                resizer.ImageResizer(orginalPath + fileName, thumbPath + fileName, width, height);
            }

            return true;
        }
        return false;
    }

    public static void DeleteImage(this string imageName, string OriginPath, string ThumbPath)
    {
        if (!string.IsNullOrEmpty(imageName))
        {
            if (File.Exists(OriginPath + imageName))
                File.Delete(OriginPath + imageName);

            if (!string.IsNullOrEmpty(ThumbPath))
            {
                if (File.Exists(ThumbPath + imageName))
                    File.Delete(ThumbPath + imageName);
            }
        }
    }
}