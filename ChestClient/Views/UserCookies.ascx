<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cookies.ascx.cs" %>

<%
   HttpCookie cookie = Request.Cookies["MyName"];

   if (cookie != null)
       Response.Write("Значение cookie получено - " + cookie.Value);
   else
       Response.Write("Значение cookie не получено");
 %>