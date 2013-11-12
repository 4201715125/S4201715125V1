namespace MUIT2013.DataMining.Discretization
{
    class Constant
    {
        //http://stackoverflow.com/questions/755685/c-static-readonly-vs-const
        public static int DecimalPlace
        {
            get { return 3; }
        }
        //Số phẩn tử trong tập dữ liệu được xem là rời rạc
        public static int DiscreteThreshold
        {
            get { return 1; }
        }
    }
}
