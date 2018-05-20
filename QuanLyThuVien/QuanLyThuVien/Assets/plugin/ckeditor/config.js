/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.height = 350;

    config.filebrowserBrowseUrl = '/Assets/plugin/ckfinder/ckfinder.html';

    config.filebrowserImageBrowseUrl = '/Assets/plugin/ckfinder/ckfinder.html?type=Images';

    config.filebrowserFlashBrowseUrl = '/Assets/plugin/ckfinder/ckfinder.html?type=Flash';

    config.filebrowserUploadUrl = '/Assets/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';

    config.filebrowserImageUploadUrl = '/Assets/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';

    config.filebrowserFlashUploadUrl = '/Assets/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

};
