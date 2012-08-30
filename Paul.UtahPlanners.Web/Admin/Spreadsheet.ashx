<%@ WebHandler Language="VB" Class="Spreadsheet" %>

Imports System
Imports System.Web

Imports com.landonpoch.data
Imports com.landonpoch.entities

Public Class Spreadsheet : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim db As New PropertiesDBDataContext
        Dim props As List(Of [Property])
        
        props = (From p In db.Properties _
                Select p).ToList()
        
        context.Response.ContentType = "application/x-download"
        context.Response.AddHeader("Content-Disposition", "attachment;filename=properties.csv")
        context.Response.Write("ID,Prop_Type,Street1,Street2,City,State,Zip,Country," & _
                               "Density,Area,Units,Street_Type,Year_Built,Socio_Econ," & _
                               "Street_Safety,Building_Enclosure,Common_Areas,Street_Connectivity," & _
                               "Street_Walkability,Walkscore,Neighborhood_Condition,250_SF,250_Apts" & vbCrLf)
        For Each prop In props
            context.Response.Write(prop.id & "," & prop.PropertyType.description & "," & _
                                   prop.Address.street1 & "," & prop.Address.street2 & "," & _
                                   prop.Address.city & "," & prop.Address.state & "," & _
                                   prop.Address.zip & "," & prop.Address.country & "," & _
                                   prop.density & "," & prop.area & "," & prop.units & "," & _
                                   prop.StreetType.description & "," & prop.yearBuilt & "," & _
                                   prop.SocioEconCode.description & "," & _
                                   prop.StreetSafteyCode.description & "," & _
                                   prop.EnclosureCode.description & "," & _
                                   prop.CommonCode.description & "," & _
                                   prop.StreetconnCode.description & "," & _
                                   prop.StreetwalkCode.description & "," & _
                                   prop.walkscore & "," & prop.NeighborhoodCode.description & "," & _
                                   prop.twoFiftySingleFam & "," & prop.twoFiftyApts & vbCrLf)
        Next
        context.Response.End()
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class