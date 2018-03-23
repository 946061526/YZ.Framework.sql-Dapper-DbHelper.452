public enum ConnectionType
{
    SQLServer = 1,
    Oracle = 2,
    OleDb = 3,
    Odbc = 4,
    MySQL = 5
}
public enum SortType
{
    Desc = 0,
    Asc = 1
}
/// <summary>
/// Amount：等于
/// GreaterThan：大于
/// GreaterOrEquals：大于等于
/// LessThan：小于
/// LessOrEquals：小于等于
/// NotEquals：不等于
/// </summary>
public enum Comparison
{
    Amount,         //=
    GreaterThan,    //>
    GreaterOrEquals, //>=
    LessThan,      //<
    LessOrEquals, //<=
    NotEquals     //<>
}
public enum OrAnd
{
    Or,
    And
}
public enum JOIN
{
    Join,
    Left_Join,
    Right_Join,
}
public enum DockState
{
    Unknown = 0,
    Float = 1,
    DockTopAutoHide = 2,
    DockLeftAutoHide = 3,
    DockBottomAutoHide = 4,
    DockRightAutoHide = 5,
    Document = 6,
    DockTop = 7,
    DockLeft = 8,
    DockBottom = 9,
    DockRight = 10,
    Hidden = 11
}

public enum DockAlignment
{
    Left,
    Right,
    Top,
    Bottom
}
public enum PageType
{
    Public,
    Internal,
    Protected,
    Private
}
public enum ChartType
{
    Line,
    Pie,
    Bar,
    None
}
public enum GisShowType
{

}