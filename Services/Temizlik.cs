// Copyright (C) 2025 Güneş Balcı, Habil Tataroğulları, Yusuf Kozan

using System;

namespace bulgarita.Services;

public static class Temizlik
{
    public static string YolaUydur(string base64string)
    {
        return base64string.Replace('/', 'ü').Replace('+', 'ş').Replace('=', 'ç');
    }
    
    public static string YolaUygundanBase64(string yolauygunbase64)
    {
        return yolauygunbase64.Replace('ç', '=').Replace('ş', '+').Replace('ü', '/');
    }
}