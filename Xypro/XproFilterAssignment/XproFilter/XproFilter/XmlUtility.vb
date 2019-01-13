Imports System.Xml
Public Class XmlUtility
    Public Shared Function IsValidXml(xml As String) As Boolean
        If IsNothing(xml) Or xml.Trim().Length = 0 Then
            Return True
        End If
        If Not (xml.Contains("<"c) And xml.Contains(">"c)) Then
            Return False
        End If
        Dim doc As XmlDocument
        doc = New XmlDocument()
        Try
            doc.LoadXml(xml)
            Return True
        Catch
            Return False
        End Try

    End Function

    Public Shared Function GetXmlString(node As XmlNode, nodename As String, Optional isattribute As Boolean = False) As String
        If IsNothing(node) = False Then
            If (isattribute = True) Then
                If IsNothing(node.Attributes) = False Then
                    For Each attr As XmlAttribute In node.Attributes
                        If attr.Name = nodename Then
                            Return attr.Value
                        End If
                    Next
                End If
            Else
                Dim subnode As XmlNode
                subnode = node.SelectSingleNode(nodename)
                If (IsNothing(subnode) = False) Then
                    Return subnode.InnerText
                End If
            End If
        End If
        Return ""
    End Function

    Public Shared Function GetXmlInteger(node As XmlNode, nodename As String, Optional isattribute As Boolean = False) As Integer
        Dim _number As String
        _number = ""
        Dim rtn As Integer
        rtn = 0
        If (IsNothing(node) = False) Then
            If (isattribute = True) Then
                If (IsNothing(node.Attributes) = False And node.Attributes.Count() > 0) Then
                    For Each attr As XmlAttribute In node.Attributes
                        If (attr.Name = nodename) Then
                            _number = attr.Value
                            Exit For
                        End If
                    Next
                End If
            Else
                Dim subnode As XmlNode
                subnode = node.SelectSingleNode(nodename)
                If (IsNothing(subnode) = False) Then
                    _number = subnode.InnerText
                End If
            End If
            If (_number.Length > 0) Then
                Try
                    rtn = Integer.Parse(_number)
                Catch
                End Try
            End If
        End If
        Return rtn
    End Function

    Public Shared Function GetXmlDateTime(node As XmlNode, nodename As String) As DateTime
        Dim dt As String
        dt = ""
        Dim rtn As DateTime
        rtn = DateTime.MinValue
        If (IsNothing(node) = False) Then
            Dim subnode As XmlNode
            subnode = node.SelectSingleNode(nodename)
            If (IsNothing(subnode) = False) Then
                dt = subnode.InnerText
            End If
        End If
        If (dt.Length > 0) Then
            Try
                rtn = DateTime.Parse(dt)
            Catch
            End Try
        End If
        Return rtn
    End Function

    Public Shared Function XmlEscape(xml As String) As String
        If (IsNothing(xml) = False And xml.Trim().Length > 0) Then
            Return xml.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("""", "&quot;").Replace("'", "&apos;")
        Else
            Return xml
        End If
    End Function
    Public Shared Function SqlString(s As String) As String
        If (IsNothing(s) = False And s.Trim().Length > 0) Then
            If (s.Contains("\\")) Then
                s = s.Replace("\\", "")
            End If
            If (s.Contains("\b")) Then
                s = s.Replace("\b", "")
            End If
            If (s.Contains("""")) Then
                s = s.Replace("""", "")
            End If
            If (s.Contains("'")) Then
                s = s.Replace("'", "''")
            End If
            If (s.Contains("\n")) Then
                s = s.Replace("\n", " ")
            End If
            If (s.Contains("\r")) Then
                s = s.Replace("\r", "")
            End If
            If (s.Contains("\t")) Then
                s = s.Replace("\t", " ")
            End If
            If (s.Contains("%")) Then
                s = s.Replace("%", "")
            End If
            If (s.Contains("_")) Then
                s = s.Replace("_", "")
            End If
        End If
        Return s
    End Function
End Class
