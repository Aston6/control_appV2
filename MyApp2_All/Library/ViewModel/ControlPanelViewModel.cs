using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
// using GC.Frame.Motion.Privt;
using MyApp2.Services;

namespace MyApp2.ViewModels
{
    public class ControlPanelViewModel : ViewModelBase
    {
        private MotionPara _motionPara = new MotionPara();
        


        public ICommand MoveFrontCommand { get; }
        public ICommand MoveBackCommand { get; }
        public ICommand MoveLeftCommand { get; }
        public ICommand MoveRightCommand { get; }
        public ICommand StopCommand { get; }

        public ControlPanelViewModel()
        {
            MoveFrontCommand = new RelayCommand(() => ExecuteDirection("Front"));
            MoveBackCommand = new RelayCommand(() => ExecuteDirection("Back"));
            MoveLeftCommand = new RelayCommand(() => ExecuteDirection("Left"));
            MoveRightCommand = new RelayCommand(() => ExecuteDirection("Right"));
            StopCommand = new RelayCommand(() => ExecuteDirection("Stop"));
        }

        private void ExecuteDirection(string direction)
        {
            if (direction != "Stop")
            {
                if (double.TryParse(VelocityText, out var v))
                    _motionPara.Vel = v;

                if (double.TryParse(AccelerationText, out var a))
                    _motionPara.Acc = a;
            }
            else
            {
                _motionPara.Vel = 0;
                _motionPara.Acc = 0;
            }


            Console.WriteLine($"Moving {direction} | Velocity: {_motionPara.Vel} | Acceleration: {_motionPara.Acc} | DevHandle: {SelectedDevHandle}");
        }


        public string VelocityText
        {
            get => _motionPara.Vel.ToString();
            set
            {
                if (double.TryParse(value, out var v))
                {
                    _motionPara.Vel = v;
                }
                OnPropertyChanged();
            }
        }

        public string AccelerationText
        {
            get => _motionPara.Acc.ToString();
            set
            {
                if (double.TryParse(value, out var a))
                {
                    _motionPara.Acc = a;
                }
                OnPropertyChanged();
            }
        }


        private ObservableCollection<string> _availableDevHandles = new()
        {
            "X", "Y", "Z", "A" // Example items, replace with your actual handles
        };

        public ObservableCollection<string> AvailableDevHandles
        {
            get => _availableDevHandles;
            set => SetProperty(ref _availableDevHandles, value);
        }

        private string _selectedDevHandle;
        public string SelectedDevHandle
        {
            get => _selectedDevHandle;
            set => SetProperty(ref _selectedDevHandle, value);
        }

        private ObservableCollection<string> _availableConnectionType = new()
        {
            "USB", "Ethernet", "RS485" // Example items, replace with your actual handles
        };

        public ObservableCollection<string> AvailableConnectionType
        {
            get => _availableConnectionType;
            set => SetProperty(ref _availableConnectionType, value);
        }

        private string _selectedConnectionType;
        public string SelectedConnectionType
        {
            get => _selectedConnectionType;
            set => SetProperty(ref _selectedConnectionType, value);
        }
    }
}





// using CommunityToolkit.Mvvm.ComponentModel;
// using CommunityToolkit.Mvvm.Input;
// using System;
// using System.Collections.ObjectModel;
// using System.Windows.Input;
// using GC.Frame.Motion.Privt;
// using System.Threading.Tasks;
// using MyApp2.Services;

// namespace MyApp2.ViewModels
// {
//     public class ControlPanelViewModel : ObservableObject
//     {
//         private readonly MotionPara[] _motionParas = new MotionPara[12]; // Array for motion parameters (up to 12 axes)
//         private readonly Axis[] _axes = new Axis[12]; // Array for axis objects
//         private ushort[] _axisHandles; // Axis handles from CNMCLib20
//         private ushort _devHandle; // Controller handle
//         private int _currentAxisIndex; // Selected axis index
//         private bool _isConnected; // Controller connection status
//         private readonly IniHelper _iniHelper = new IniHelper(); // INI file helper
//         private readonly string _iniPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "runparam.ini");
//         private readonly object _lockObj = new object(); // For thread safety

//         // Properties for UI binding
//         public ObservableCollection<string> AvailableDevHandles { get; } = new ObservableCollection<string>();
//         private string _selectedDevHandle;
//         public string SelectedDevHandle
//         {
//             get => _selectedDevHandle;
//             set
//             {
//                 if (SetProperty(ref _selectedDevHandle, value))
//                 {
//                     _currentAxisIndex = AvailableDevHandles.IndexOf(value);
//                     UpdateUI();
//                 }
//             }
//         }

//         public ObservableCollection<string> AvailableConnectionType { get; } = new ObservableCollection<string> { "Ethernet" }; // Only Ethernet supported in original code
//         private string _selectedConnectionType;
//         public string SelectedConnectionType
//         {
//             get => _selectedConnectionType;
//             set => SetProperty(ref _selectedConnectionType, value);
//         }

//         private string _velocityText;
//         public string VelocityText
//         {
//             get => _velocityText;
//             set
//             {
//                 if (double.TryParse(value, out var v))
//                 {
//                     _motionParas[_currentAxisIndex].Vel = v;
//                     _iniHelper.IniWriteValue(_axisHandles[_currentAxisIndex].ToString(), "VEL", value);
//                 }
//                 SetProperty(ref _velocityText, value);
//             }
//         }

//         private string _accelerationText;
//         public string AccelerationText
//         {
//             get => _accelerationText;
//             set
//             {
//                 if (double.TryParse(value, out var a))
//                 {
//                     _motionParas[_currentAxisIndex].Acc = a;
//                     _iniHelper.IniWriteValue(_axisHandles[_currentAxisIndex].ToString(), "ACC", value);
//                 }
//                 SetProperty(ref _accelerationText, value);
//             }
//         }

//         private string _positionText;
//         public string PositionText
//         {
//             get => _positionText;
//             set
//             {
//                 if (double.TryParse(value, out var p))
//                 {
//                     _motionParas[_currentAxisIndex].Pos = p;
//                     _iniHelper.IniWriteValue(_axisHandles[_currentAxisIndex].ToString(), "POS", value);
//                 }
//                 SetProperty(ref _positionText, value);
//             }
//         }

//         private int _motionMode; // 0: Jog, 1: Relative PTP, 2: Absolute PTP
//         public int MotionMode
//         {
//             get => _motionMode;
//             set
//             {
//                 if (SetProperty(ref _motionMode, value))
//                 {
//                     _motionParas[_currentAxisIndex].Mode = value;
//                     _iniHelper.IniWriteValue(_axisHandles[_currentAxisIndex].ToString(), "MODE", value.ToString());
//                 }
//             }
//         }

//         private bool _isAxisEnabled;
//         public bool IsAxisEnabled
//         {
//             get => _isAxisEnabled;
//             set => SetProperty(ref _isAxisEnabled, value);
//         }

//         private string _statusText = "提示:未连接上控制器";
//         public string StatusText
//         {
//             get => _statusText;
//             set => SetProperty(ref _statusText, value);
//         }

//         // Commands
//         public IRelayCommand ConnectCommand { get; }
//         public IRelayCommand ToggleAxisCommand { get; }
//         public IRelayCommand MoveRightCommand { get; }
//         public IRelayCommand MoveLeftCommand { get; }
//         public IRelayCommand StopCommand { get; }

//         public ControlPanelViewModel()
//         {
//             for (int i = 0; i < _axes.Length; i++)
//             {
//                 _axes[i] = new Axis();
//                 _motionParas[i] = new MotionPara();
//             }

//             ConnectCommand = new RelayCommand(ConnectToController);
//             ToggleAxisCommand = new RelayCommand(ToggleAxis);
//             MoveRightCommand = new RelayCommand(async () => await ExecuteMove("Right"), () => _isConnected && IsAxisEnabled);
//             MoveLeftCommand = new RelayCommand(async () => await ExecuteMove("Left"), () => _isConnected && IsAxisEnabled);
//             StopCommand = new RelayCommand(Stop, () => _isConnected);

//             _iniHelper.inipath = _iniPath;
//             ConnectToController(); // Attempt to connect on initialization
//         }

//         private void ConnectToController()
//         {
//             short rtn = 0;
//             short devNum = 0;
//             byte[] devInfo = new byte[4 * 84];
//             rtn = CNMCLib20.NMC_DevSearch(CNMCLib20.TSearchMode.Ethernet, ref devNum, devInfo);
//             if (rtn == 0 && devNum > 0)
//             {
//                 rtn = CNMCLib20.NMC_DevOpen(0, ref _devHandle);
//                 if (rtn != 0)
//                 {
//                     StatusText = "提示:未连接上控制器";
//                     return;
//                 }

//                 _isConnected = true;
//                 StatusText = "提示:控制器已连接";

//                 // Get controller info
//                 var devInfoStruct = new CNMCLib20.TCardInfo();
//                 rtn = CNMCLib20.NMC_GetCardInfo(_devHandle, ref devInfoStruct);
//                 int numAxes = devInfoStruct.axisNum;

//                 // Initialize axes
//                 _axisHandles = new ushort[numAxes];
//                 AvailableDevHandles.Clear();
//                 for (int i = 0; i < numAxes; i++)
//                 {
//                     AvailableDevHandles.Add(i.ToString());
//                     rtn = CNMCLib20.NMC_MtOpen(_devHandle, (short)i, ref _axisHandles[i]);
//                     _motionParas[i] = GetMotionPara(_axisHandles[i]);
//                     _axes[i] = GetAxisPara(_devHandle, _axisHandles[i], _motionParas[i].Scale, _motionParas[i].Smoth);
//                 }

//                 // Set default axis
//                 if (AvailableDevHandles.Count > 0)
//                 {
//                     SelectedDevHandle = AvailableDevHandles[0];
//                     UpdateUI();
//                 }
//             }
//             else
//             {
//                 StatusText = "提示:未连接上控制器";
//             }
//         }

//         private void ToggleAxis()
//         {
//             short rtn = 0;
//             if (!_isAxisEnabled)
//             {
//                 rtn = CNMCLib20.NMC_MtSetSvOn(_axisHandles[_currentAxisIndex]);
//                 if (rtn == 0)
//                 {
//                     IsAxisEnabled = true;
//                     StatusText = $"轴{_currentAxisIndex}使能成功";
//                 }
//                 else
//                 {
//                     StatusText = $"轴{_currentAxisIndex}使能失败: {rtn}";
//                 }
//             }
//             else
//             {
//                 rtn = CNMCLib20.NMC_MtSetSvOff(_axisHandles[_currentAxisIndex]);
//                 if (rtn == 0)
//                 {
//                     IsAxisEnabled = false;
//                     StatusText = $"轴{_currentAxisIndex}卸载使能成功";
//                 }
//                 else
//                 {
//                     StatusText = $"轴{_currentAxisIndex}卸载使能失败: {rtn}";
//                 }
//             }
//         }

//         private async Task ExecuteMove(string direction)
//         {
//             short rtn = 0;
//             var axisHandle = _axisHandles[_currentAxisIndex];
//             var motionPara = _motionParas[_currentAxisIndex];
//             var axis = _axes[_currentAxisIndex];

//             if (motionPara.Mode == 0) // Jog mode
//             {
//                 rtn = CNMCLib20.NMC_MtSetPrfMode(axisHandle, CNMCLib20.MT_JOG_PRF_MODE);
//                 if (rtn != 0)
//                 {
//                     StatusText = "JOG模式设置失败";
//                     return;
//                 }

//                 CNMCLib20.TJogPara jogPara = new()
//                 {
//                     acc = motionPara.Acc * motionPara.Scale / 1000000,
//                     dec = motionPara.Dec * motionPara.Scale / 1000000,
//                     smoothCoef = axis.Smooth
//                 };
//                 rtn = CNMCLib20.NMC_MtSetJogPara(axisHandle, ref jogPara);
//                 if (rtn != 0)
//                 {
//                     StatusText = "JOG参数设置失败";
//                     return;
//                 }

//                 double velocity = direction == "Right" ? motionPara.Vel : -motionPara.Vel;
//                 rtn = CNMCLib20.NMC_MtSetVel(axisHandle, velocity * motionPara.Scale / 1000);
//                 if (rtn != 0)
//                 {
//                     StatusText = "JOG速度设置失败";
//                     return;
//                 }

//                 rtn = CNMCLib20.NMC_MtUpdate(axisHandle);
//                 if (rtn != 0)
//                 {
//                     StatusText = $"JOG启动失败: {rtn}";
//                     return;
//                 }

//                 // Simulate button hold (wait for release or stop command)
//                 while (_axes[_currentAxisIndex].IsRuning)
//                 {
//                     _axes[_currentAxisIndex] = GetAxisPara(_devHandle, axisHandle, motionPara.Scale, motionPara.Smoth);
//                     await Task.Delay(100); // Poll axis status
//                 }
//             }
//             else // PTP mode (relative or absolute)
//             {
//                 double targetPos;
//                 if (motionPara.Mode == 1) // Relative PTP
//                 {
//                     double currentPos = _axes[_currentAxisIndex].CurretPos0 * motionPara.Scale;
//                     targetPos = direction == "Right" ? currentPos + motionPara.Pos * motionPara.Scale : currentPos - motionPara.Pos * motionPara.Scale;
//                 }
//                 else // Absolute PTP
//                 {
//                     targetPos = direction == "Right" ? motionPara.Pos * motionPara.Scale : -motionPara.Pos * motionPara.Scale;
//                 }

//                 rtn = CNMCLib20.NMC_MtSetPrfMode(axisHandle, CNMCLib20.MT_PTP_PRF_MODE);
//                 if (rtn != 0)
//                 {
//                     StatusText = "PTP模式设置失败";
//                     return;
//                 }

//                 CNMCLib20.TPtpPara ptpPara = new()
//                 {
//                     acc = motionPara.Acc * motionPara.Scale / 1000000,
//                     dec = motionPara.Dec * motionPara.Scale / 1000000,
//                     smoothCoef = (short)axis.Smooth,
//                     startVel = motionPara.JumpVel * motionPara.Scale / 1000,
//                     endVel = motionPara.EndVel * motionPara.Scale / 1000
//                 };
//                 rtn = CNMCLib20.NMC_MtSetPtpPara(axisHandle, ref ptpPara);
//                 if (rtn != 0)
//                 {
//                     StatusText = "PTP参数设置失败";
//                     return;
//                 }

//                 rtn = CNMCLib20.NMC_MtSetVel(axisHandle, motionPara.Vel * motionPara.Scale / 1000);
//                 if (rtn != 0)
//                 {
//                     StatusText = "PTP速度设置失败";
//                     return;
//                 }

//                 rtn = CNMCLib20.NMC_MtSetPtpTgtPos(axisHandle, (int)targetPos);
//                 if (rtn != 0)
//                 {
//                     StatusText = "PTP目标位置设置失败";
//                     return;
//                 }

//                 rtn = CNMCLib20.NMC_MtUpdate(axisHandle);
//                 if (rtn != 0)
//                 {
//                     StatusText = $"PTP启动失败: {rtn}";
//                 }
//             }
//         }

//         private void Stop()
//         {
//             short rtn = CNMCLib20.NMC_MtEstp(_axisHandles[_currentAxisIndex]);
//             if (rtn == 0)
//             {
//                 StatusText = "提示:运动停止";
//             }
//             else
//             {
//                 StatusText = $"提示:停止失败: {rtn}";
//             }
//         }

//         private void UpdateUI()
//         {
//             if (_currentAxisIndex >= 0 && _currentAxisIndex < _motionParas.Length)
//             {
//                 var motionPara = _motionParas[_currentAxisIndex];
//                 var axis = _axes[_currentAxisIndex];
//                 VelocityText = motionPara.Vel.ToString();
//                 AccelerationText = motionPara.Acc.ToString();
//                 PositionText = motionPara.Pos.ToString();
//                 MotionMode = motionPara.Mode;
//                 IsAxisEnabled = axis.IsAxisOn;
//             }
//         }

//         private MotionPara GetMotionPara(ushort axisHandle)
//         {
//             MotionPara motionPara = new MotionPara
//             {
//                 AxisNumber = axisHandle,
//                 // Pos = Convert.ToDouble(_iniHelper.IniReadValue(axisHandle.ToString(), "POS", "100")),
//                 // Vel = Convert.ToDouble(_iniHelper.IniReadValue(axisHandle.ToString(), "VEL", "50")),
//                 // Acc = Convert.ToDouble(_iniHelper.IniReadValue(axisHandle.ToString(), "ACC", "1000")),
//                 // Dec = Convert.ToDouble(_iniHelper.IniReadValue(axisHandle.ToString(), "DEC", "1000")),
//                 // JumpVel = Convert.ToDouble(_iniHelper.IniReadValue(axisHandle.ToString(), "JUMPVEL", "0")),
//                 // EndVel = Convert.ToDouble(_iniHelper.IniReadValue(axisHandle.ToString(), "ENDVEL", "0")),
//                 // Mode = Convert.ToInt32(_iniHelper.IniReadValue(axisHandle.ToString(), "MODE", "0")),
//                 // Scale = Convert.ToDouble(_iniHelper.IniReadValue(axisHandle.ToString(), "SCALE", "1000")),
//                 // ZeroPos = Convert.ToInt32(_iniHelper.IniReadValue(axisHandle.ToString(), "ZEROPOS", "1")),
//                 // Smoth = Convert.ToInt32(_iniHelper.IniReadValue(axisHandle.ToString(), "SMOTH", "0"))
//             };
//             return motionPara;
//         }

//         private Axis GetAxisPara(ushort devHandle, ushort axisHandle, double scale, double smoothTime)
//         {
//             lock (_lockObj)
//             {
//                 short rtn = 0;
//                 short axisStatus = 0;
//                 int posTemp = 0;
//                 short posSwitch = 0, negSwitch = 0, switch1 = 0, switch2 = 0;
//                 short encMode = 0;
//                 int posLimit = 0, negLimit = 0;
//                 int posError = 0;
//                 short pos = 0, neg = 0, pos2 = 0, neg2 = 0;
//                 double currentVel = 0.0;
//                 double scaleTemp = scale == 0 ? 1.0 : scale;
//                 CNMCLib20.THomeSetting homePara = new();
//                 CNMCLib20.TSafePara safePara = new();
//                 Axis temp = new Axis { AxisHandle = axisHandle, Scale = scaleTemp };

//                 // Limit switch settings
//                 rtn = CNMCLib20.NMC_MtGetLmtOnOff(axisHandle, ref posSwitch, ref negSwitch);
//                 temp.IsNegLmtActived = negSwitch == 1;
//                 temp.IsPosLmtActived = posSwitch == 1;
//                 rtn = CNMCLib20.NMC_MtGetLmtSns(axisHandle, ref posSwitch, ref negSwitch);
//                 temp.IsNeglmtDown = negSwitch == 0;
//                 temp.IsPoslmtDown = posSwitch == 0;

//                 // Alarm settings
//                 rtn = CNMCLib20.NMC_MtGetAlarmOnOff(axisHandle, ref switch1);
//                 temp.IsAlarmActived = switch1 == 1;
//                 rtn = CNMCLib20.NMC_MtGetAlarmSns(axisHandle, ref switch1);
//                 temp.IsAlarmDown = switch1 == 0;

//                 // Pulse mode
//                 rtn = CNMCLib20.NMC_MtGetStepMode(axisHandle, ref switch1, ref switch2);
//                 temp.IsEncNeged = switch1 == 1;
//                 temp.StepMode = switch2;

//                 // Axis status
//                 rtn = CNMCLib20.NMC_MtGetSts(axisHandle, ref axisStatus);
//                 temp.IsRuning = (axisStatus & (1 << 0)) != 0;
//                 temp.IsArrive = (axisStatus & (1 << 1)) != 0;
//                 temp.IsError = (axisStatus & (1 << 2)) != 0;
//                 temp.IsAxisOn = (axisStatus & (1 << 3)) != 0;
//                 temp.PosArrived = (axisStatus & (1 << 6)) != 0;
//                 temp.NegArrived = (axisStatus & (1 << 7)) != 0;
//                 temp.PosSoftArrived = (axisStatus & (1 << 8)) != 0;
//                 temp.NegSoftArrived = (axisStatus & (1 << 9)) != 0;
//                 temp.IsAlarming = (axisStatus & (1 << 10)) != 0;
//                 temp.IsPosErr = (axisStatus & (1 << 11)) != 0;

//                 // Position and velocity
//                 rtn = CNMCLib20.NMC_MtGetPrfPos(axisHandle, ref posTemp);
//                 temp.CurretPos0 = posTemp / scaleTemp;
//                 rtn = CNMCLib20.NMC_MtGetAxisPos(axisHandle, ref posTemp);
//                 temp.CurretPos1 = posTemp / scaleTemp;
//                 rtn = CNMCLib20.NMC_MtGetPrfVel(axisHandle, ref currentVel);
//                 temp.CurrentVel = currentVel * 1000 / scaleTemp;

//                 // Soft limit and safety parameters
//                 rtn = CNMCLib20.NMC_MtGetSwLmtOnOff(axisHandle, ref switch1);
//                 temp.IsSoftLmtActived = switch1 == 1;
//                 rtn = CNMCLib20.NMC_MtGetSwLmtValue(axisHandle, ref posLimit, ref negLimit);
//                 temp.SafePoslmtPos = posLimit / scaleTemp;
//                 temp.SafelNeglmtPos = negLimit / scaleTemp;
//                 rtn = CNMCLib20.NMC_MtGetSafePara(axisHandle, ref safePara);
//                 temp.StopDec = safePara.estpDec * 1000 * 1000 / scaleTemp;
//                 temp.MaxVel = safePara.maxVel * 1000 / scaleTemp;
//                 temp.MaxaAcc = safePara.maxAcc * 1000 * 1000 / scaleTemp;
//                 temp.Smooth = smoothTime;
//                 rtn = CNMCLib20.NMC_MtGetPosErrLmt(axisHandle, ref posError);
//                 temp.MaxPosErr = posError;

//                 // Encoder mode
//                 rtn = CNMCLib20.NMC_GetEncMode(devHandle, (short)axisHandle, ref encMode);
//                 temp.EncMode = encMode switch
//                 {
//                     0 => ENCMODE.OUTAB,
//                     1024 => ENCMODE.OUTPD,
//                     2048 => ENCMODE.OUTPG,
//                     4096 => ENCMODE.OUTABFU,
//                     17408 => ENCMODE.OUTPDFU,
//                     6144 => ENCMODE.OUTPGFU,
//                     256 => ENCMODE.INAB,
//                     1280 => ENCMODE.INPD,
//                     2304 => ENCMODE.INPG,
//                     4352 => ENCMODE.INABFU,
//                     17664 => ENCMODE.INPDFU,
//                     6400 => ENCMODE.INPGFU,
//                     _ => ENCMODE.INAB
//                 };

//                 // Homing parameters
//                 rtn = CNMCLib20.NMC_MtGetHomePara(axisHandle, ref homePara);
//                 temp.HomeMaxPos = homePara.safeLen / scaleTemp;
//                 temp.HomeAcc = homePara.acc * 1000000 / scaleTemp;
//                 temp.SerchHomeVel = homePara.scan1stVel * 1000 / scaleTemp;
//                 temp.HomeOffset = homePara.offset / scaleTemp;
//                 temp.HomeBackVel = homePara.scan2ndVel * 1000 / scaleTemp;
//                 temp.IsNegHome = homePara.dir == 0;
//                 temp.HomeMode = homePara.mode;
//                 temp.IsHomeTwice = homePara.reScanEn == '1';
//                 temp.IsZUp = homePara.zEdge == '1';
//                 temp.IsLmtUp = homePara.lmtEdge == '1';
//                 temp.IsHomeUp = homePara.homeEdge == '1';
//                 temp.HomeoffsetBegin = homePara.iniRetPos / scaleTemp;
//                 temp.HomeOffsetLmt = homePara.retSwOffset / scaleTemp;

//                 return temp;
//             }
//         }

//         private void NewInifile(int count)
//         {
//             int n = 257;
//             for (int i = n; i < count + n; i++)
//             {
//                 _iniHelper.IniWriteValue(i.ToString(), "POS", "100");
//                 _iniHelper.IniWriteValue(i.ToString(), "VEL", "50");
//                 _iniHelper.IniWriteValue(i.ToString(), "ACC", "1000");
//                 _iniHelper.IniWriteValue(i.ToString(), "DEC", "1000");
//                 _iniHelper.IniWriteValue(i.ToString(), "JUMPVEL", "0");
//                 _iniHelper.IniWriteValue(i.ToString(), "ENDVEL", "0");
//                 _iniHelper.IniWriteValue(i.ToString(), "MODE", "0");
//                 _iniHelper.IniWriteValue(i.ToString(), "SCALE", "1000");
//                 _iniHelper.IniWriteValue(i.ToString(), "ZEROPOS", "1");
//                 _iniHelper.IniWriteValue(i.ToString(), "SMOTH", "0");
//             }
//         }
//     }
// }