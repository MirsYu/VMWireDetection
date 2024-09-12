using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.IO.Ports;

namespace VMWireDetection
{
    public class HSL_ModbusRTU
    {
        private ModbusRtu _RTU = new ModbusRtu();

        public bool Open(string PortName, int BaudRate, int DataBits, StopBits stopBits, Parity parity)
        {
            try
            {
                _RTU.SerialPortInni(sp =>
                {
                    sp.PortName = PortName;
                    sp.BaudRate = BaudRate;
                    sp.DataBits = DataBits;
                    sp.StopBits = stopBits;
                    sp.Parity = parity;
                });
                OperateResult result = _RTU.Open();
                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                LogManager.WriteError(ex.Message);
                return false;
            }
        }

        public bool Close()
        {
            _RTU.Close();
            return true;
        }

        public bool Read<T>(string Address, ref T Result)
        {
            if (Global._Main.IsRunMode)
            {
                try
                {
                    if (_RTU.IsOpen())
                    {
                        T result = default(T);
                        switch (typeof(T).Name)
                        {
                            case "Int32":
                                result = (T)(object)_RTU.ReadInt32(Address).Content;
                                break;
                            case "Single":
                                result = (T)(object)_RTU.ReadFloat(Address).Content;
                                break;
                            case "String":
                                result = (T)(object)_RTU.ReadString(Address, 1).Content;
                                break;
                            default:
                                return false;
                        }
                        Result = result;
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    LogManager.WriteError(ex.Message);
                    return false;
                }
            }
            return false;
        }

        public bool Write<T>(string Address, T Value)
        {
            if (Global._Main.IsRunMode)
            {
                try
                {
                    if (_RTU.IsOpen())
                    {
                        switch (typeof(T).Name)
                        {
                            case "Int32":
                                _RTU.Write(Address, Convert.ToInt32(Value));
                                break;
                            case "Single":
                                _RTU.Write(Address, Convert.ToSingle(Value));
                                break;
                            case "String":
                                _RTU.Write(Address, Value.ToString());
                                break;
                            default:
                                return false;
                        }
                        return true;
                    }
                    return false;

                }
                catch (Exception ex)
                {
                    LogManager.WriteError(ex.Message);
                    return false;
                }
            }
            return false;
        }

    }
}
