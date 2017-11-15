Option Strict Off
Option Explicit On

Imports System.Xml
Imports System.Text
Imports VB = Microsoft.VisualBasic

Module moduleKML

    Public Sub ExportKML(ByVal filename As String)


        'Dim settings As XmlWriterSettings = New XmlWriterSettings()
        'settings.Indent = True
        'settings.Encoding = Encoding.GetEncoding(28591)
        'settings.NewLineOnAttributes = True

        'Dim writer As XmlWriter = XmlWriter.Create(filename, settings)
        'writer.WriteStartDocument()
        'writer.WriteComment("Created by SBuilderX on " & Now)
        'writer.WriteStartElement("FSData")
        'writer.WriteAttributeString("version", "9.0")
        'writer.WriteAttributeString("xmlns", "xsi", Nothing, "http://www.w3.org/2001/XMLSchema-instance")
        'writer.WriteAttributeString("noNamespaceSchemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "bglcomp.xsd")

        'writer.WriteComment("FSX library objects")
        'For N = 1 To NoOfObjects
        '    If Objects(N).Selected Then
        '        If Objects(N).Type = 0 Then
        '            ObjLibType = 2
        '            AnalyseLibObject(N)
        '            ObjComment = ObjComment & "_Obj_" & CStr(N)
        '            writer.WriteComment(ObjComment)
        '            writer.WriteStartElement("SceneryObject")
        '            writer.WriteAttributeString("lat", Trim(Str(Objects(N).lat)))
        '            writer.WriteAttributeString("lon", Trim(Str(Objects(N).lon)))
        '            writer.WriteAttributeString("alt", Trim(Str(Objects(N).Altitude)))
        '            writer.WriteAttributeString("altitudeIsAgl", GetTR(Objects(N).AGL))
        '            writer.WriteAttributeString("pitch", Trim(Str(Objects(N).Pitch)))
        '            writer.WriteAttributeString("bank", Trim(Str(Objects(N).Bank)))
        '            writer.WriteAttributeString("heading", Trim(Str(Objects(N).Heading)))
        '            writer.WriteAttributeString("imageComplexity", GetComplex(Objects(N).Complexity))
        '            BiasX = Objects(N).BiasX
        '            BiasY = Objects(N).BiasY
        '            BiasZ = Objects(N).BiasZ
        '            If BiasX <> 0 Or BiasY <> 0 Or BiasZ <> 0 Then
        '                writer.WriteStartElement("BiasXYZ")
        '                writer.WriteAttributeString("biasX", Trim(Str(BiasX)))
        '                writer.WriteAttributeString("biasY", Trim(Str(BiasY)))
        '                writer.WriteAttributeString("biasZ", Trim(Str(BiasZ)))
        '                writer.WriteEndElement()
        '            End If
        '            writer.WriteStartElement("LibraryObject")
        '            writer.WriteAttributeString("name", ObjLibID)
        '            writer.WriteAttributeString("scale", Trim(Str(ObjLibScale)))
        '            writer.WriteEndElement()
        '            writer.WriteFullEndElement()
        '        End If
        '    End If
        'Next


    End Sub
End Module

