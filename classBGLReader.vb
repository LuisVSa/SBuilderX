Public Class BGLReader

    Structure MDL
        Dim Guid As String
        Dim Name As String
        Dim W As Single
        Dim L As Single
        Dim Type As Integer
    End Structure

    Private MDLsValue As MDL()
    Public ReadOnly Property MDLs() As MDL()
        Get
            Return MDLsValue
        End Get
    End Property

    Private NoOfMDLsValue As Integer
    Public ReadOnly Property NoOfMDLs() As Integer
        Get
            Return NoOfMDLsValue
        End Get
    End Property

    Private TypeValue As Integer
    Public ReadOnly Property Type() As Integer
        Get
            Return TypeValue
        End Get
    End Property

    Public Function read(ByVal reader As BinaryReader) As Boolean

        Dim NoOfSPs As Integer  ' number of section pointers
        Dim NoOfSubSPs As Integer  ' number of sub section pointers
        Dim NoOfRs As Integer ' number of records
        Dim N, J, K As Integer
        Dim SOffset As UInt32 ' section offset from begin of stream
        Dim SubSOffset As UInt32 ' sub section offset from begin of stream
        Dim mdlOffset As UInt32 ' mdl record offset from begin of section
        Dim SType As UInt32
        Dim SPos As Long  ' pointer to store section position
        Dim RecPos As Long  ' pointer to store record position

        Dim B As Byte()
        Dim G As Guid

        read = False
        NoOfMDLsValue = 0
        Dim mdl As New MDLReader

        Dim myMDL As MDL

        Try

            If reader.ReadUInt16 <> 513 Then Exit Function ' bgl ide
            If reader.ReadUInt16 <> 6546 Then Exit Function ' version number

            reader.ReadBytes(16)  ' skip 16 bytes
            NoOfSPs = CInt(reader.ReadUInt32)
            reader.ReadBytes(32)  ' skip 132 bytes

            For N = 1 To NoOfSPs ' read all section pointers

                SType = reader.ReadUInt32
                reader.ReadBytes(4)  ' skip 4 bytes
                NoOfSubSPs = CInt(reader.ReadUInt32)
                SOffset = reader.ReadUInt32
                reader.ReadBytes(4)  ' skip 4 bytes - size of header

                If SType = 43 Then  ' section type = 2b (mdl)

                    reader.BaseStream.Position = SOffset ' point to begin of section

                    For J = 1 To NoOfSubSPs    ' read all section headers
                        reader.ReadBytes(4)  ' skip ID - 4 bytes
                        NoOfRs = CInt(reader.ReadUInt32())
                        SubSOffset = reader.ReadUInt32()
                        reader.ReadBytes(4)  ' skip size - 4 bytes

                        SPos = reader.BaseStream.Position
                        reader.BaseStream.Position = SubSOffset
                        ReDim MDLsValue(NoOfRs)
                        For K = 1 To NoOfRs  ' read all records

                            B = reader.ReadBytes(16)
                           
                            mdlOffset = reader.ReadUInt32()
                            reader.ReadBytes(4) ' skip size

                            RecPos = reader.BaseStream.Position
                            reader.BaseStream.Position = SPos + mdlOffset

                            If mdl.Read(reader) Then
                                TypeValue = mdl.Type
                                myMDL.Type = TypeValue
                                NoOfMDLsValue = NoOfMDLsValue + 1
                                If TypeValue = 1 Then ' for FS9
                                    G = New Guid(B)
                                    myMDL.Guid = G.ToString("N")
                                    myMDL.Name = "_#" & NoOfMDLsValue.ToString
                                Else ' it is fsx
                                    myMDL.Guid = mdl.Guid  ' ignore the guid in BGL and use the one in the mdl
                                    myMDL.Name = mdl.Name
                                End If
                                myMDL.W = mdl.Width
                                myMDL.L = mdl.Lenght
                                MDLsValue.SetValue(myMDL, NoOfMDLsValue)
                            End If
                            reader.BaseStream.Position = RecPos
                        Next
                        reader.BaseStream.Position = SPos
                    Next
                End If
            Next

            read = True

        Catch ex As Exception

        End Try

    End Function

End Class
