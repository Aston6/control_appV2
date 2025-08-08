using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GC.Frame.Motion.Privt;

namespace MyApp2.Services
{
    public enum ENCMODE : int
    {
        OUTAB, //外部，AB相90度差
        OUTPD, //外部，脉冲加方向
        OUTPG, //外部，正负脉冲
        OUTABFU,//外部，AB相90度差（负逻辑）
        OUTPDFU,//外部，脉冲加方向（负逻辑）
        OUTPGFU,//外部，正负脉冲（负逻辑）
        INAB,//内部，AB相90度差
        INPD,//内部，脉冲加方向
        INPG,//内部，正负脉冲
        INABFU,//内部，AB相90度差（负逻辑）
        INPDFU,//内部，脉冲加方向（负逻辑）
        INPGFU,//内部，正负脉冲（负逻辑）
    }
    /// <summary>
    /// 记录轴配置的类
    /// </summary>
    public class Axis
    {
        private UInt16 axisHandle;
        private bool isPosLmtActived=false;
        private bool isNegLmtActived=false;
        private bool isAlarmActived=false;
        private int stepMode;
        private bool isAxisOn;
        private bool isAlarming;
        private bool isAlarmDown=true;
        private bool isRuning;
        private bool isPosErr;
        private bool negArrived;
        private bool posSoftArrived;
        private bool negSoftArrived;
        private bool posArrived;
        private bool isArrive;
        private bool isError;
        private double curretPos0;
        private double curretPos1;
        private double currentVel;
        private bool isNeglmtDown=true;
        private bool isPoslmtDown=true;
        private bool isSoftLmtActived=false;
        private double safePoslmtPos;
        private double safelNeglmtPos;
        private double smooth;
        private double stopDec;
        private double maxaAcc;
        private double maxVel;
        private int maxPosErr;
        private bool isEncNeged;
        private ENCMODE encMode;
        private double homeMaxPos;
        private double serchHomeVel;
        private double homeBackVel;
        private double homeOffset;
        private double homeAcc;
        private bool isNegHome=true;
        private bool isHomeTwice=false;
        private bool isHomeZero=true;
        private bool isHomeUp=false;
        private bool isLmtUp=false;
        private bool isZUp=false;
        private double scale;
        private short homeMode;
        private double homeoffsetBegin;
        private double homeOffsetLmt;
        /// <summary>
        /// 轴句柄
        /// </summary>
        public UInt16 AxisHandle { get => axisHandle; set => axisHandle = value; }
        /// <summary>
        /// 正限位是否激活
        /// </summary>
        public bool IsPosLmtActived { get => isPosLmtActived; set => isPosLmtActived = value; }
        /// <summary>
        /// 负限位是否激活
        /// </summary>
        public bool IsNegLmtActived { get => isNegLmtActived; set => isNegLmtActived = value; }
        /// <summary>
        /// 驱动器报警是否激活
        /// </summary>
        public bool IsAlarmActived { get => isAlarmActived; set => isAlarmActived = value; }
        /// <summary>
        /// 脉冲模式，0，脉冲加方向，1，正负脉冲
        /// </summary>
        public int StepMode { get => stepMode; set => stepMode = value; }
        /// <summary>
        /// 驱动器是否使能
        /// </summary>
        public bool IsAxisOn { get => isAxisOn; set => isAxisOn = value; }
        /// <summary>
        /// 是否正在报警
        /// </summary>
        public bool IsAlarming { get => isAlarming; set => isAlarming = value; }
        
        /// <summary>
        /// 是否正在运动
        /// </summary>
        public bool IsRuning { get => isRuning; set => isRuning = value; }
        /// <summary>
        /// 是否位置越限
        /// </summary>
        public bool IsPosErr { get => isPosErr; set => isPosErr = value; }
        /// <summary>
        /// 负限位是否触发
        /// </summary>
        public bool NegArrived { get => negArrived; set => negArrived = value; }
        /// <summary>
        /// 正限位是否触发
        /// </summary>
        public bool PosArrived { get => posArrived; set => posArrived = value; }
        
        /// <summary>
        /// 是否运动到位
        /// </summary>
        public bool IsArrive { get => isArrive; set => isArrive = value; }
        /// <summary>
        /// 是否运动报错
        /// </summary>
        public bool IsError { get => isError; set => isError = value; }
        /// <summary>
        /// 当前计数器位置
        /// </summary>
        public double CurretPos0 { get => curretPos0; set => curretPos0 = value; }
        /// <summary>
        /// 当前编码器位置
        /// </summary>
        public double CurretPos1 { get => curretPos1; set => curretPos1 = value; }
        /// <summary>
        /// 当前速度
        /// </summary>
        public double CurrentVel { get => currentVel; set => currentVel = value; }
        /// <summary>
        /// 正限位是否为低电平触发
        /// </summary>
        public bool IsPoslmtDown { get => isPoslmtDown; set => isPoslmtDown = value; }
        /// <summary>
        /// 负限位是否为低电平触发
        /// </summary>
        public bool IsNeglmtDown { get => isNeglmtDown; set => isNeglmtDown = value; }
        /// <summary>
        /// 软限位是否激活
        /// </summary>
        public bool IsSoftLmtActived { get => isSoftLmtActived; set => isSoftLmtActived = value; }
        /// <summary>
        /// 软限位安全位置正
        /// </summary>
        public double SafePoslmtPos { get => safePoslmtPos; set => safePoslmtPos = value; }
        /// <summary>
        /// 软限位安全位置负
        /// </summary>
        public double SafelNeglmtPos { get => safelNeglmtPos; set => safelNeglmtPos = value; }
        /// <summary>
        /// 平滑系数
        /// </summary>
        public double Smooth { get => smooth; set => smooth = value; }
        /// <summary>
        /// 急停减速度
        /// </summary>
        public double StopDec { get => stopDec; set => stopDec = value; }
        /// <summary>
        /// 最大加速度
        /// </summary>
        public double MaxaAcc { get => maxaAcc; set => maxaAcc = value; }
        /// <summary>
        /// 最大速度
        /// </summary>
        public double MaxVel { get => maxVel; set => maxVel = value; }       
        /// <summary>
        /// 允许的最大位置误差
        /// </summary>
        public int MaxPosErr { get => maxPosErr; set => maxPosErr = value; }
        /// <summary>
        /// 编码器计数模式
        /// </summary>
         public ENCMODE EncMode { get => encMode; set => encMode = value; }
        /// <summary>
        /// 指令脉冲是否取反
        /// </summary>
        public bool IsEncNeged { get => isEncNeged; set => isEncNeged = value; }
        /// <summary>
        /// 原点最大搜索距离
        /// </summary>
        public double HomeMaxPos { get => homeMaxPos; set => homeMaxPos = value; }
        /// <summary>
        /// 搜索速度
        /// </summary>
        public double SerchHomeVel { get => serchHomeVel; set => serchHomeVel = value; }
        /// <summary>
        /// 原点返回速度
        /// </summary>
        public double HomeBackVel { get => homeBackVel; set => homeBackVel = value; }
        /// <summary>
        /// 回零偏移量
        /// </summary>
        public double HomeOffset { get => homeOffset; set => homeOffset = value; }
        /// <summary>
        /// 回零加速度
        /// </summary>
        public double HomeAcc { get => homeAcc; set => homeAcc = value; }
        /// <summary>
        /// 是否负向回零
        /// </summary>
        public bool IsNegHome { get => isNegHome; set => isNegHome = value; }
        /// <summary>
        /// 是否二次回零
        /// </summary>
        public bool IsHomeTwice { get => isHomeTwice; set => isHomeTwice = value; }
        /// <summary>
        /// 是否位置需要清零
        /// </summary>
        public bool IsHomeZero { get => isHomeZero; set => isHomeZero = value; }
        /// <summary>
        /// 原点信号是否上升沿触发
        /// </summary>
        public bool IsHomeUp { get => isHomeUp; set => isHomeUp = value; }
        /// <summary>
        /// 限位信号是否上升沿触发
        /// </summary>
        public bool IsLmtUp { get => isLmtUp; set => isLmtUp = value; }
        /// <summary>
        /// Z向信号是否上升沿触发
        /// </summary>
        public bool IsZUp { get => isZUp; set => isZUp = value; }
        /// <summary>
        /// 是否是低电平触发驱动器报警
        /// </summary>
        public bool IsAlarmDown { get => isAlarmDown; set => isAlarmDown = value; }
        /// <summary>
        /// 负向软限位是否触发
        /// </summary>
        public bool NegSoftArrived { get => negSoftArrived; set => negSoftArrived = value; }
        /// <summary>
        /// 正向软限位是否触发
        /// </summary>
        public bool PosSoftArrived { get => posSoftArrived; set => posSoftArrived = value; }
        /// <summary>
        /// 轴当量
        /// </summary>
        public double Scale { get => scale; set => scale = value; }
        /// <summary>
        /// 回零模式选择
        /// </summary>
        public short HomeMode { get => homeMode; set => homeMode = value; }
        /// <summary>
        /// 起始反向距离
        /// </summary>
        public double HomeoffsetBegin { get => homeoffsetBegin; set => homeoffsetBegin = value; }
        /// <summary>
        /// 离开开关距离
        /// </summary>
        public double HomeOffsetLmt { get => homeOffsetLmt; set => homeOffsetLmt = value; }      
    }
}
