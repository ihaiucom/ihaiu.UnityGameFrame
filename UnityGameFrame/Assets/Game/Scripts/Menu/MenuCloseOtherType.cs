
namespace Games
{
    /** 关闭其他面板方式 */
    public enum MenuCloseOtherType 
    {
        /** 不关闭任何面板 */
        None,

        /** 除自己外的所有 */
        ExceptSelf_All,

        /** 除自己外的所有模块层级面板 */
        ExceptSelf_Module,

        /** 相同层级的其他面板 */
        ExceptSelf_SameLayer,
    }
}
