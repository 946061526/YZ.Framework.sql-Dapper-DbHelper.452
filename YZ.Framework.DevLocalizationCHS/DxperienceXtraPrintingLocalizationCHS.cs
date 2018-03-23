﻿using DevExpress.XtraPrinting.Localization;

namespace YZ.Framework.DevLocalizationCHS
{

    public class DxperienceXtraPrintingLocalizationCHS : PreviewLocalizer
    {
        public override string GetLocalizedString(PreviewStringId id)
        {
            switch (id)
            {
                case PreviewStringId.Button_Cancel:
                    return "取消";
                case PreviewStringId.Button_Ok:
                    return "确定";
                case PreviewStringId.Button_Help:
                    return "帮助";
                case PreviewStringId.Button_Apply:
                    return "应用";
                case PreviewStringId.PreviewForm_Caption:
                    return "预览";
                case PreviewStringId.TB_TTip_Customize:
                    return "自定义";
                case PreviewStringId.TB_TTip_Print:
                    return "打印";
                case PreviewStringId.TB_TTip_PrintDirect:
                    return "直接打印";
                case PreviewStringId.TB_TTip_PageSetup:
                    return "页面设置";
                case PreviewStringId.TB_TTip_Magnifier:
                    return "放大/缩小";
                case PreviewStringId.TB_TTip_ZoomIn:
                    return "放大";
                case PreviewStringId.TB_TTip_ZoomOut:
                    return "缩小";
                case PreviewStringId.TB_TTip_Zoom:
                    return "缩放";
                case PreviewStringId.TB_TTip_Search:
                    return "搜索";
                case PreviewStringId.TB_TTip_FirstPage:
                    return "首页";
                case PreviewStringId.TB_TTip_PreviousPage:
                    return "上一页";
                case PreviewStringId.TB_TTip_NextPage:
                    return "下一页";
                case PreviewStringId.TB_TTip_LastPage:
                    return "尾页";
                case PreviewStringId.TB_TTip_MultiplePages:
                    return "多页";
                case PreviewStringId.TB_TTip_Backgr:
                    return "背景色";
                case PreviewStringId.TB_TTip_Close:
                    return "退出";
                case PreviewStringId.TB_TTip_EditPageHF:
                    return "页眉页脚";
                case PreviewStringId.TB_TTip_HandTool:
                    return "手掌工具";
                case PreviewStringId.TB_TTip_Export:
                    return "导出文件";
                case PreviewStringId.TB_TTip_Send:
                    return "发送e-mail";
                case PreviewStringId.TB_TTip_Map:
                    return "文档视图";
                case PreviewStringId.TB_TTip_Watermark:
                    return "水印";
                case PreviewStringId.MenuItem_PdfDocument:
                    return "PDF文件";
                case PreviewStringId.MenuItem_PageLayout:
                    return "页面布置(&P)";
                case PreviewStringId.MenuItem_TxtDocument:
                    return "Text文件";
                case PreviewStringId.MenuItem_GraphicDocument:
                    return "图片文件";
                case PreviewStringId.MenuItem_CsvDocument:
                    return "CSV文件";
                case PreviewStringId.MenuItem_MhtDocument:
                    return "MHT文件";
                case PreviewStringId.MenuItem_XlsDocument:
                    return "Excel文件";
                case PreviewStringId.MenuItem_RtfDocument:
                    return "Rich Text文件";
                case PreviewStringId.MenuItem_HtmDocument:
                    return "HTML文件";
                case PreviewStringId.SaveDlg_FilterBmp:
                    return "BMP Bitmap Format";
                case PreviewStringId.SaveDlg_FilterGif:
                    return "GIF Graphics Interchange Format";
                case PreviewStringId.SaveDlg_FilterJpeg:
                    return "JPEG File Interchange Format";
                case PreviewStringId.SaveDlg_FilterPng:
                    return "PNG Portable Network Graphics Format";
                case PreviewStringId.SaveDlg_FilterTiff:
                    return "TIFF Tag Image File Format";
                case PreviewStringId.SaveDlg_FilterEmf:
                    return "EMF Enhanced Windows Metafile";
                case PreviewStringId.SaveDlg_FilterWmf:
                    return "WMF Windows Metafile";
                case PreviewStringId.PageInfo_PageTotal:
                    return "总页码:";
                case PreviewStringId.SB_PageOfPages:
                    return "目前页码:";
                case PreviewStringId.SB_ZoomFactor:
                    return "缩放系数:";
                case PreviewStringId.SB_PageNone:
                    return "无";
                case PreviewStringId.SB_PageInfo:
                    return "{0}/{1}";
                case PreviewStringId.MPForm_Lbl_Pages:
                    return "页";
                case PreviewStringId.Msg_EmptyDocument:
                    return "此文件没有页面.";
                case PreviewStringId.Msg_CreatingDocument:
                    return "正在生成文件";
                case PreviewStringId.Msg_UnavailableNetPrinter:
                    return "网络打印机无法使用.";
                case PreviewStringId.Msg_NeedPrinter:
                    return "没有安装打印机.";
                case PreviewStringId.Msg_WrongPrinter:
                    return "无效的打印机名称.请检查打印机的设置是否正确.";
                case PreviewStringId.Msg_WrongPageSettings:
                    return "打印机不支持所选的纸张大小.\n是否继续打印？";
                case PreviewStringId.Msg_CustomDrawWarning:
                    return "警告！";
                case PreviewStringId.Msg_PageMarginsWarning:
                    return "一个或以上的边界超出了打印范围.是否继续？";
                case PreviewStringId.Msg_IncorrectPageRange:
                    return "设置的页边界不正确";
                case PreviewStringId.Msg_FontInvalidNumber:
                    return "字体大小不能为0或负数";
                case PreviewStringId.Msg_NotSupportedFont:
                    return "这种字体不被支持";
                case PreviewStringId.Msg_IncorrectZoomFactor:
                    return "数字必须在 {0} 与 {1} 之间。";
                case PreviewStringId.Msg_InvalidMeasurement:
                    return "不规范";
                case PreviewStringId.Margin_Inch:
                    return "英寸";
                case PreviewStringId.Margin_Millimeter:
                    return "毫米";
                case PreviewStringId.Margin_TopMargin:
                    return "上边界";
                case PreviewStringId.Margin_BottomMargin:
                    return "下边界";
                case PreviewStringId.Margin_LeftMargin:
                    return "左边界";
                case PreviewStringId.Margin_RightMargin:
                    return "右边界";
                case PreviewStringId.ScrollingInfo_Page:
                    return "页";
                case PreviewStringId.WMForm_PictureDlg_Title:
                    return "选择图片";
                case PreviewStringId.WMForm_ImageStretch:
                    return "伸展";
                case PreviewStringId.WMForm_ImageClip:
                    return "剪辑";
                case PreviewStringId.WMForm_ImageZoom:
                    return "缩放";
                case PreviewStringId.WMForm_Watermark_Asap:
                    return "ASAP";
                case PreviewStringId.WMForm_Watermark_Confidential:
                    return "CONFIDENTIAL";
                case PreviewStringId.WMForm_Watermark_Copy:
                    return "COPY";
                case PreviewStringId.WMForm_Watermark_DoNotCopy:
                    return "DO NOT COPY";
                case PreviewStringId.WMForm_Watermark_Draft:
                    return "DRAFT";
                case PreviewStringId.WMForm_Watermark_Evaluation:
                    return "EVALUATION";
                case PreviewStringId.WMForm_Watermark_Original:
                    return "ORIGINAL";
                case PreviewStringId.WMForm_Watermark_Personal:
                    return "PERSONAL";
                case PreviewStringId.WMForm_Watermark_Sample:
                    return "SAMPLE";
                case PreviewStringId.WMForm_Watermark_TopSecret:
                    return "TOP SECRET";
                case PreviewStringId.WMForm_Watermark_Urgent:
                    return "URGENT";
                case PreviewStringId.WMForm_Direction_Horizontal:
                    return "横向";
                case PreviewStringId.WMForm_Direction_Vertical:
                    return "纵向";
                case PreviewStringId.WMForm_Direction_BackwardDiagonal:
                    return "反向倾斜";
                case PreviewStringId.WMForm_Direction_ForwardDiagonal:
                    return "正向倾斜";
                case PreviewStringId.WMForm_VertAlign_Bottom:
                    return "底端";
                case PreviewStringId.WMForm_VertAlign_Middle:
                    return "中间";
                case PreviewStringId.WMForm_VertAlign_Top:
                    return "顶端";
                case PreviewStringId.WMForm_HorzAlign_Left:
                    return "靠左";
                case PreviewStringId.WMForm_HorzAlign_Center:
                    return "置中";
                case PreviewStringId.WMForm_HorzAlign_Right:
                    return "靠右";                
                case PreviewStringId.SaveDlg_Title:
                    return "另存为";
                case PreviewStringId.SaveDlg_FilterPdf:
                    return "PDF文件";
                case PreviewStringId.SaveDlg_FilterTxt:
                    return "Txt文件";
                case PreviewStringId.SaveDlg_FilterCsv:
                    return "CSV文件";
                case PreviewStringId.SaveDlg_FilterMht:
                    return "MHT文件";
                case PreviewStringId.SaveDlg_FilterXls:
                    return "Excel文件";
                case PreviewStringId.SaveDlg_FilterRtf:
                    return "Rtf文件";
                case PreviewStringId.SaveDlg_FilterHtm:
                    return "HTML文件";
                case PreviewStringId.MenuItem_File:
                    return "文件(&F)";
                case PreviewStringId.MenuItem_View:
                    return "视图(&V)";
                case PreviewStringId.MenuItem_Background:
                    return "背景(&B)";
                case PreviewStringId.MenuItem_PageSetup:
                    return "页面设置(&U)";
                case PreviewStringId.MenuItem_Print:
                    return "打印(&P)";
                case PreviewStringId.MenuItem_PrintDirect:
                    return "直接打印(&R)";
                case PreviewStringId.MenuItem_Export:
                    return "导出(&E)";
                case PreviewStringId.MenuItem_Send:
                    return "发送(&D)";
                case PreviewStringId.MenuItem_Exit:
                    return "退出(&X)";
                case PreviewStringId.MenuItem_ViewToolbar:
                    return "工具栏(&T)";
                case PreviewStringId.MenuItem_ViewStatusbar:
                    return "状态栏(&S)";
                case PreviewStringId.MenuItem_ViewContinuous:
                    return "连续";
                case PreviewStringId.MenuItem_ViewFacing:
                    return "对页";
                case PreviewStringId.MenuItem_BackgrColor:
                    return "颜色(&C)";
                case PreviewStringId.MenuItem_Watermark:
                    return "水印(&W)";
                case PreviewStringId.MenuItem_ZoomPageWidth:
                    return "页宽";
                case PreviewStringId.MenuItem_ZoomTextWidth:
                    return "整页";
                case PreviewStringId.PageInfo_PageNumber:
                    return "[Page #]";
                case PreviewStringId.PageInfo_PageNumberOfTotal:
                    return "[Page # of Pages #]";
                case PreviewStringId.PageInfo_PageDate:
                    return "[Date Printed]";
                case PreviewStringId.PageInfo_PageTime:
                    return "[Time Printed]";
                case PreviewStringId.PageInfo_PageUserName:
                    return "[User Name]";
                case PreviewStringId.EMail_From:
                    return "From";
                case PreviewStringId.BarText_Toolbar:
                    return "工具栏";
                case PreviewStringId.BarText_MainMenu:
                    return "主菜单";
                case PreviewStringId.BarText_StatusBar:
                    return "状态栏";
            }
            return base.GetLocalizedString(id);
        }
        public override string Language
        {
            get
            {
                return "简体中文";
            }
        }
    }
}
