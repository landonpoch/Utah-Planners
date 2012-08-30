<%@ WebHandler Language="VB" Class="Upload" %>

Imports System
Imports System.Web
Imports System.Web.SessionState
Imports com.landonpoch.entities

Public Class Upload : Implements IHttpHandler, IRequiresSessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        Try
            
            Dim sesid = context.Session.SessionID
            
            Dim db = context.Session("db")
            Dim prop = context.Session("prop")
            Dim pic As New Picture()
        
            Dim file = context.Request.Files("Filedata")
            Dim buffer(file.ContentLength) As Byte
            file.InputStream.Read(buffer, 0, file.ContentLength)
        
            pic.binaryData = buffer
            pic.mimeType = "image/jpeg"
            pic.mainPicture = context.Request("mainPicture")
            pic.secondaryPicture = context.Request("secondaryPicture")
            pic.frontPage = context.Request("frontPage")
        
            prop.Pictures.Add(pic)
            
            
            context.Session("prop") = prop
            context.Session("db") = db
            context.Session("newPic") = pic
        
            context.Response.ContentType = "text/plain"
            context.Response.Write("Upload Successful")
        Catch ex As Exception
            context.Response.ContentType = "text/plain"
            context.Response.Write("Upload Failed")
        End Try
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class