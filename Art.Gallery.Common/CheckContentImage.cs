﻿using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Drawing;
namespace Art.Gallery.Common;
public static class CheckContentImage
{
    public const int ImageMinimumBytes = 512;
    public static bool IsImage(this IFormFile postedFile)
    {
        if (postedFile.ContentType.ToLower() != "image/jpg" &&
                    postedFile.ContentType.ToLower() != "image/jpeg" &&
                    postedFile.ContentType.ToLower() != "image/pjpeg" &&
                    postedFile.ContentType.ToLower() != "image/x-png" &&
                    postedFile.ContentType.ToLower() != "image/png" &&
                    postedFile.ContentType.ToLower() != "image/gif")
        {
            return false;
        }
        if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg"
            && Path.GetExtension(postedFile.FileName).ToLower() != ".gif")
        {
            return false;
        }
        try
        {
            if (!postedFile.OpenReadStream().CanRead)
            {
                return false;
            }
            if (postedFile.Length < ImageMinimumBytes)
            {
                return false;
            }
            byte[] buffer = new byte[ImageMinimumBytes];
            postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
            string content = System.Text.Encoding.UTF8.GetString(buffer);
            if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
        try
        {
            using (var bitmap = new Bitmap(postedFile.OpenReadStream()))
            {
            }
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            postedFile.OpenReadStream().Position = 0;
        }
        return true;
    }
}