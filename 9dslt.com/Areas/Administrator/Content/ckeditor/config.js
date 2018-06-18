/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.lang = 'en';
    config.toolbar = 'Full';
    config.filebrowserBrowseUrl = "/Areas/Administrator/Content/ckfinder/ckfinder.html";
    config.filebrowserImageUrl = "/Areas/Administrator/Content/ckfinder/ckfinder.html?type=Images";
    config.filebrowserFlashUrl = "/Areas/Administrator/Content/ckfinder/ckfinder.html?type=Flash";
    config.filebrowserUploadUrl = "/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserImageUploadUrl = "/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = "/Areas/Administrator/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";
};
