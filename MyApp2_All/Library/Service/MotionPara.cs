namespace MyApp2.Services
{
    public class MotionPara
    {
        private ushort axisNumber =0;
        private double pos=0.0;
        private double vel=0.0;
        private double acc=0.0;
        private double dec=0.0;
        private double jumpVel=0.0;
        private double endVel=0.0;
        private int mode=0;
        private double scale=0.0;
        private int zeroPos=0;
        private double smoth=0.0;

        public ushort AxisNumber { get => axisNumber; set => axisNumber = value; }
        public double Pos { get => pos; set => pos = value; }
        public double Vel { get => vel; set => vel = value; }
        public double Acc { get => acc; set => acc = value; }
        public double Dec { get => dec; set => dec = value; }
        public double JumpVel { get => jumpVel; set => jumpVel = value; }
        public double EndVel { get => endVel; set => endVel = value; }
        public int Mode { get => mode; set => mode = value; }
        public double Scale { get => scale; set => scale = value; }
        public int ZeroPos { get => zeroPos; set => zeroPos = value; }
        public double Smoth { get => smoth; set => smoth = value; }

    }
}