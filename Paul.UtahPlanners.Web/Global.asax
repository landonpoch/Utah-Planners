<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
    Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        'we guess at this point session is not already retrieved by application so we recreate cookie with the session id...
        Try
            Dim session_param_name = "ASPSESSID"
            Dim session_cookie_name = "ASP.NET_SessionId"

            If Not HttpContext.Current.Request.Form(session_param_name) Is Nothing Then
                UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form(session_param_name))
            ElseIf Not HttpContext.Current.Request.QueryString(session_param_name) Is Nothing Then
                UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString(session_param_name))
            End If
        Catch ex As Exception
        End Try
        

        Try
            Dim auth_param_name = "AUTHID"
            Dim auth_cookie_name = FormsAuthentication.FormsCookieName

            If Not HttpContext.Current.Request.Form(auth_param_name) Is Nothing Then
                UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form(auth_param_name))
            ElseIf Not HttpContext.Current.Request.QueryString(auth_param_name) Is Nothing Then
                UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString(auth_param_name))
            End If
        catch ex As Exception
        End Try
    End Sub
    
    Private Sub UpdateCookie(ByVal cookie_name As String, ByVal cookie_value As String)
        Dim cookie = HttpContext.Current.Request.Cookies.Get(cookie_name)
        If cookie Is Nothing Then
            cookie = New HttpCookie(cookie_name)
        End If
        cookie.Value = cookie_value
        HttpContext.Current.Request.Cookies.Set(cookie)
    End Sub
    
</script>