<%@ Page Language="c#" Inherits="CKFinder.Connector.Connector" Trace="false" AutoEventWireup="false" %>
<%@ Register Src="~/Areas/Administrator/Content/ckfinder/config.ascx" TagName="Config" TagPrefix="CKFinder" %>

<%--<uc1:config runat="server" id="config" />
 * <%@ Register Src="../../../config.ascx" TagName="Config" TagPrefix="CKFinder" %>
 * CKFinder
 * ========
 * http://cksource.com/ckfinder
 * Copyright (C) 2007-2014, CKSource - Frederico Knabben. All rights reserved.
 *
 * The software, this file and its contents are subject to the CKFinder
 * License. Please read the license.txt file before using, installing, copying,
 * modifying or distribute this file or part of its contents. The contents of
 * this file is part of the Source Code of CKFinder.
 *
 * ---
 * This is the ASP.NET connector for CKFinder.
 *
 * You must copy the CKFinder.Connector.dll file to your "bin" directory or
 * make a reference to it in your Visual Studio project.
--%>
<CKFinder:Config ID="ConfigFile" runat="server" />
