/*
Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
For licensing, see license.txt or http://cksource.com/ckfinder/license
*/

CKFinder.customConfig = function( config )
{
	// Define changes to default configuration here.
	// For the list of available options, check:
	// http://docs.cksource.com/ckfinder_2.x_api/symbols/CKFinder.config.html

	// Sample configuration options:
	// config.uiColor = '#BDE31E';
	// config.language = 'fr';
    // config.removePlugins = 'basket';
    config.lang = 'en';
    config.filebrowserBrowseUrl = '/Areas/Administrator/Content/ckfinder/ckfinder.html',
    config.filebrowserImageUrl = '/Areas/Administrator/Content/ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashUrl = '/Areas/Administrator/Content/ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

};
