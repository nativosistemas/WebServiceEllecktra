using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Diagnostics;
using System.Reflection;


public enum dllEstadosPedido
{
    Anulado = 0,
    EnPreparacion = 1,
    EnSucursal = 2,
    Enviado = 3,
    PendienteDeFacturar = 4,
    Detenido = 5
}
public enum dllEstadoCheque
{
    Aceptado = 0,
    Cambiado = 1,
    Depositado = 2,
    EnCartera = 3,
    Rechazado = 4,
    Retirado = 5
}
public enum dllTipoComprobante
{
    NN = -1,
    FAC = 0,
    REC = 1,
    NDE = 2,
    NCR = 3,
    NDI = 4,
    NCI = 5,
    RES = 9,
    CIE = 10,
    OSC = 13
}

public enum dllMotivoDevolucion
{
    BienFacturadoMalEnviado = 1,
    ProductoMalEstado = 2,
    FacturadoNoPedido = 3,
    ProductoDeMasSinSerFacturado = 4,
    VencimientoCorto = 5,
    ProductoFallaFabricante = 6,
    Vencido = 7
}

public class cResumen
{
    public string Numero { get; set; }
    public string NumeroSemana { get; set; }
    public DateTime? PeriodoDesde { get; set; }
    public string PeriodoDesdeToString { get; set; }
    public DateTime? PeriodoHasta { get; set; }
    public string PeriodoHastaToString { get; set; }
    public decimal? TotalResumen { get; set; }
    public List<cResumenDetalle> lista { get; set; }
}
public class cResumenDetalle
{
    public string Descripcion { get; set; }
    public string Dia { get; set; }
    public string Importe { get; set; }
    public string NumeroHoja { get; set; }
    public int NumeroItem { get; set; }
    public string NumeroResumen { get; set; }
    public string TipoComprobante { get; set; }
    //public string TipoComprobanteToString { get; set; }
}
public class cCtaCteMovimiento
{
    public string Atraso { get; set; }
    public DateTime? Fecha { get; set; }
    public string FechaToString { get; set; }
    public DateTime? FechaPago { get; set; }
    public string FechaPagoToString { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public string FechaVencimientoToString { get; set; }
    public decimal? Importe { get; set; }
    public string MedioPago { get; set; }
    public string NumeroComprobante { get; set; }
    public string NumeroRecibo { get; set; }
    public string Pago { get; set; }
    public decimal? Saldo { get; set; }
    public string Semana { get; set; }
    public dllTipoComprobante TipoComprobante { get; set; }
    public string TipoComprobanteToString { get; set; }
    public List<cVencimientoResumen> lista { get; set; }
}

public class cVencimientoResumen
{
    public string Tipo { get; set; }
    public string NumeroComprobante { get; set; }
    public DateTime? Fecha { get; set; }
    public string FechaToString { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public string FechaVencimientoToString { get; set; }
    public double? Importe { get; set; }
}

public class cFichaCtaCte
{
    public DateTime Fecha { get; set; }
    public string FechaToString { get; set; }
    //public dkInterfaceWeb.TiposComprobante TipoComprobante { get; set; }
    public dllTipoComprobante TipoComprobante { get; set; }
    public string TipoComprobanteToString { get; set; }
    public string Comprobante { get; set; }
    public string Motivo { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public string FechaVencimientoToString { get; set; }
    public decimal? Debe { get; set; }
    public decimal? Haber { get; set; }
    public decimal? Saldo { get; set; }
}
public class cComprobanteDiscriminado
{
    public string Comprobante { get; set; }
    public string Destinatario { get; set; }
    public string DetallePercepciones { get; set; }
    public DateTime Fecha { get; set; }
    public string FechaToString { get; set; }
    public decimal MontoExento { get; set; }
    public decimal MontoGravado { get; set; }
    public decimal MontoIvaInscripto { get; set; }
    public decimal MontoIvaNoInscripto { get; set; }
    public decimal MontoPercepcionDGR { get; set; }
    public decimal MontoTotal { get; set; }
    public string NumeroComprobante { get; set; }
}
public class cNotaDeDebito
{
    public string CantidadHojas { get; set; }
    public string Destinatario { get; set; }
    public DateTime? Fecha { get; set; }
    public string FechaToString { get; set; }
    public decimal MontoExento { get; set; }
    public decimal MontoGravado { get; set; }
    public decimal MontoIvaInscripto { get; set; }
    public decimal MontoIvaNoInscripto { get; set; }
    public decimal MontoPercepcionDGR { get; set; }
    public decimal MontoTotal { get; set; }
    public string Motivo { get; set; }
    public string Numero { get; set; }
    public List<cNotaDeDebitoDetalle> lista { get; set; }
}
public class cNotaDeDebitoDetalle
{
    public string Descripcion { get; set; }
    public string Importe { get; set; }
    public string NumeroHoja { get; set; }
    public int NumeroItem { get; set; }
    public string NumeroNotaDeDebito { get; set; }

}
public class cNotaDeCredito
{
    public string CantidadHojas { get; set; }
    public string Destinatario { get; set; }
    public DateTime? Fecha { get; set; }
    public string FechaToString { get; set; }
    public decimal MontoExento { get; set; }
    public decimal MontoGravado { get; set; }
    public decimal MontoIvaInscripto { get; set; }
    public decimal MontoIvaNoInscripto { get; set; }
    public decimal MontoPercepcionDGR { get; set; }
    public decimal MontoTotal { get; set; }
    public string Motivo { get; set; }
    public string Numero { get; set; }
    public int TotalUnidades { get; set; }
    public List<cNotaDeCreditoDetalle> lista { get; set; }
}
public class cNotaDeCreditoDetalle
{
    public string Cantidad { get; set; }
    public string Descripcion { get; set; }
    public string Importe { get; set; }
    public string NumeroHoja { get; set; }
    public int NumeroItem { get; set; }
    public string NumeroNotaDeCredito { get; set; }
    public string PrecioPublico { get; set; }
    public string PrecioUnitario { get; set; }
    public string Troquel { get; set; }

}
public class cFactura
{
    public string CantidadHojas { get; set; }
    public int CantidadRenglones { get; set; }
    public string CodigoFormaDePago { get; set; }
    public decimal DescuentoEspecial { get; set; }
    public decimal DescuentoNetos { get; set; }
    public decimal DescuentoPerfumeria { get; set; }
    public decimal DescuentoWeb { get; set; }
    public string Destinatario { get; set; }
    public DateTime? Fecha { get; set; }
    public string FechaToString { get; set; }
    public decimal MontoExento { get; set; }
    public decimal MontoGravado { get; set; }
    public decimal MontoIvaInscripto { get; set; }
    public decimal MontoIvaNoInscripto { get; set; }
    public decimal MontoPercepcionDGR { get; set; }
    public decimal MontoTotal { get; set; }
    public string Numero { get; set; }
    public int NumeroCuentaCorriente { get; set; }
    public string NumeroRemito { get; set; }
    public int TotalUnidades { get; set; }
    public decimal MontoPercepcionMunicipal { get; set; }
    public bool? FacturaTrazable { get; set; }
    public List<cFacturaDetalle> lista { get; set; }
}
public class cFacturaDetalle
{
    public string Cantidad { get; set; }
    public string Caracteristica { get; set; }
    public string Descripcion { get; set; }
    public string Importe { get; set; }
    public string NumeroFactura { get; set; }
    public string NumeroHoja { get; set; }
    public int NumeroItem { get; set; }
    public string PrecioPublico { get; set; }
    public string PrecioUnitario { get; set; }
    public string Troquel { get; set; }

}
public class Resultado
{
    public Resultado()
    {
        estado = -1;
        obj = null;
    }
    public Resultado(int pEstado)
    {
        estado = pEstado;
        obj = null;
    }
    public Resultado(int pEstado, Object pObj)
    {
        estado = pEstado;
        obj = pObj;
    }
    public int estado { get; set; }
    public object obj { get; set; }
}
public class cDllPedidoItem
{
    public int Cantidad { get; set; }
    public string Caracteristica { get; set; }
    public int Faltas { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public string FechaIngresoToString { get; set; }
    public string NombreObjetoComercial { get; set; }
}

public class cDllProductosAndCantidad
{
    public string codProductoNombre { get; set; }
    public int IdTransfer { get; set; }
    public int cantidad { get; set; }
    public bool isOferta { get; set; }
}
public class cDllPedido
{
    public int CantidadRenglones { get; set; }
    public int CantidadUnidad { get; set; }
    public string Error { get; set; }
    public dllEstadosPedido Estado { get; set; }
    public string EstadoToString { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public string FechaIngresoToString { get; set; }
    public string FechaIngresoHoraToString { get; set; }
    public List<cDllPedidoItem> Items { get; set; }
    public List<cDllPedidoItem> ItemsConProblemasDeCreditos { get; set; }
    public string Login { get; set; }
    public string MensajeEnFactura { get; set; }
    public string MensajeEnRemito { get; set; }
    public decimal MontoTotal { get; set; }
    public string NumeroFactura { get; set; }
    public string NumeroRemito { get; set; }
    public bool Cancelado { get; set; }
    public string DetalleSucursal { get; set; }
}
public class cDllPedidoTransfer
{
    public int CantidadRenglones { get; set; }
    public int CantidadUnidad { get; set; }
    public string Error { get; set; }
    public dllEstadosPedido Estado { get; set; }
    public string EstadoToString { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public string FechaIngresoToString { get; set; }
    public string FechaIngresoHoraToString { get; set; }
    public List<cDllPedidoItem> Items { get; set; }
    public List<cDllPedidoItem> ItemsConProblemasDeCreditos { get; set; }
    public string Login { get; set; }
    public string MensajeEnFactura { get; set; }
    public string MensajeEnRemito { get; set; }
    public decimal MontoTotal { get; set; }
    public string NumeroFactura { get; set; }
    public string NumeroRemito { get; set; }
    public bool Cancelado { get; set; }
}
public class cDllCtaResumenMovimiento
{
    public DateTime Fecha { get; set; }
    public string FechaToString { get; set; }
    public dllTipoComprobante TipoComprobante { get; set; }
    public string TipoComprobanteToString { get; set; }
    public string NumeroComprobante { get; set; }
    public decimal Importe { get; set; }

}
public class cDllRespuestaResumenAbierto
{
    public List<cDllCtaResumenMovimiento> lista { get; set; }
    public bool isPoseeCuenta { get; set; }
    public decimal ImporteTotal { get; set; }
}
public class cDllChequeRecibido
{
    public string Banco { get; set; }
    public dllEstadoCheque Estado { get; set; }
    public string EstadoToString { get; set; }
    public string Fecha { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public string FechaVencimientoToString { get; set; }
    public decimal Importe { get; set; }
    public string Numero { get; set; }
}
public class cDllSaldosComposicion
{
    public decimal? SaldoTotal { get; set; }
    public decimal? SaldoCtaCte { get; set; }
    public decimal? SaldoResumenAbierto { get; set; }
    public decimal? SaldoChequeCartera { get; set; }
    public bool isPoseeCuentaResumen { get; set; }
}
public class cComprobantesDiscriminadosDePuntoDeVenta
{
    public string Comprobante { get; set; }
    public string Destinatario { get; set; }
    //lista.get_Item(0).DetallePercepciones;
    public DateTime Fecha { get; set; }
    public string FechaToString { get; set; }
    public decimal MontoExento { get; set; }
    public decimal MontoGravado { get; set; }
    public decimal MontoIvaInscripto { get; set; }
    public decimal MontoIvaNoInscripto { get; set; }
    public decimal MontoPercepcionesDGR { get; set; }
    public decimal MontoTotal { get; set; }
    public decimal MontoPercepcionesMunicipal { get; set; }
    public string NumeroComprobante { get; set; }

}
public class cPlan
{
    public string Nombre { get; set; }
    public bool PideSemana { get; set; }
}
public class cPlanillaObSoc
{
    public string Anio { get; set; }
    public DateTime Fecha { get; set; }
    public string FechaToString { get; set; }
    public decimal Importe { get; set; }
    public string Mes { get; set; }
    public string Quincena { get; set; }
    public string Semana { get; set; }

}
public class cObraSocialCliente
{
    public string CantidadHojas { get; set; }
    public string Destinatario { get; set; }
    public DateTime Fecha { get; set; }
    public string FechaToString { get; set; }
    public decimal MontoTotal { get; set; }
    public string NombrePlan { get; set; }
    public int NumeroPlanilla { get; set; }
    //public string NumeroObraSocialCliente { get; set; }
    public List<cObraSocialClienteItem> lista { get; set; }

}
public class cObraSocialClienteItem
{
    public string Descripcion { get; set; }
    public string Importe { get; set; }
    public string NumeroHoja { get; set; }
    public int NumeroItem { get; set; }
    public string NumeroObraSocialCliente { get; set; }
}
public class cCbteParaImprimir
{
    public DateTime FechaComprobante { get; set; }
    public string FechaComprobanteToString { get; set; }
    public string NumeroComprobante { get; set; }
    public string TipoComprobante { get; set; }
}
public class cConsObraSocial
{
    public  string Detalle { get; set; }
    public DateTime FechaComprobante { get; set; }
    public string FechaComprobanteToString { get; set; }
    public  decimal Importe { get; set; }
    public  string NumeroComprobante { get; set; }
    public  string TipoComprobante { get; set; }
}

public class cLote
{
    public string ID { get; set; }
    public string NombreProducto { get; set; }
    public string NumeroLote { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public string FechaVencimientoToString { get; set; }
}

public class cDevolucionItemPrecarga
{
    public int dev_numeroitem { get; set; }
    public int dev_numerocliente { get; set; }
    public string dev_numerofactura { get; set; }
    public string dev_numerosolicituddevolucion { get; set; }
    public string dev_nombreproductodevolucion { get; set; }
    public DateTime dev_fecha { get; set; }
    public string dev_fechaToString { get; set; }
    public dllMotivoDevolucion dev_motivo { get; set; }
    public int dev_numeroitemfactura { get; set; }
    public string dev_nombreproductofactura { get; set; }
    public double dev_cantidad { get; set; }
    public string dev_numerolote { get; set; }
    public DateTime dev_fechavencimientolote { get; set; }
    public string dev_fechavencimientoloteToString { get; set; }
    public string dev_estado { get; set; }
    public string dev_mensaje { get; set; }
    public double dev_cantidadrecibida { get; set; }
    public double dev_cantidadrechazada { get; set; }
    public string dev_idsucursal { get; set; }
    public string dev_numerosolicitudNC { get; set; }
}

public class Autenticacion : SoapHeader
{
    private string sUserPass;
    private string sUserName;

    /// <summary> 
    /// Lee o escribe la clave del usuario 
    /// </summary> 
    public string UsuarioClave
    {
        get
        {
            return sUserPass;
        }
        set
        {
            sUserPass = value;
        }

    }
    /// <summary> 
    /// Lee o escribe el nombre del usuario 
    /// </summary> 
    public string UsuarioNombre
    {
        get
        {
            return sUserName;
        }
        set
        {
            sUserName = value;
        }
    }
}
public static class Serializador
{
    public static string SerializarToXml(object obj)
    {
        try
        {
            StringWriter strWriter = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            serializer.Serialize(strWriter, obj);
            string resultXml = strWriter.ToString();
            strWriter.Close();

            return resultXml;
        }
        catch
        {
            return string.Empty;
        }
    }
    //Deserializar un XML a un objeto T 
    public static T DeserializarToXml<T>(string xmlSerializado)
    {
        try
        {
            XmlSerializer xmlSerz = new XmlSerializer(typeof(T));

            using (StringReader strReader = new StringReader(xmlSerializado))
            {
                object obj = xmlSerz.Deserialize(strReader);
                return (T)obj;
            }
        }
        catch { return default(T); }
    }
    /// <summary> 
    /// Método extensor para serializar JSON cualquier objeto 
    /// </summary> 
    public static string SerializarAJson(this object objeto)
    {
        string jsonResultado = string.Empty;
        try
        {
            DataContractJsonSerializer jsonSerializar = new DataContractJsonSerializer(objeto.GetType());
            MemoryStream ms = new MemoryStream();
            jsonSerializar.WriteObject(ms, objeto);
            jsonResultado = Encoding.UTF8.GetString(ms.ToArray());
        }
        catch { throw; }
        return jsonResultado;
    }
    public static T DeserializarJson<T>(this string jsonSerializado)
    {
        try
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonSerializado));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            ms.Dispose();
            return obj;
        }
        catch { return default(T); }
    }

}
public class classTiempo
{
    Stopwatch stopWatch = new Stopwatch();
    string nombre = string.Empty;
    DateTime FechaActual;
    public classTiempo(string NombreFuncion)
    {
        nombre = NombreFuncion;
        FechaActual = DateTime.Now;
        stopWatch.Start();
    }
    public void Parar()
    {
        try
        {
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string path = @"c:/" + @"LogTiempoWebService" + @"/";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string nombreArchivo = "tiempo.txt";
            string FechaToString = FechaActual.Year.ToString("0000") + "_" + FechaActual.Month.ToString("00") + "_" + FechaActual.Day.ToString("00") + "_h_" + FechaActual.Hour.ToString("00") + "_" + FechaActual.Minute.ToString("00") + "_" + FechaActual.Second.ToString("00") + "_" + FechaActual.Millisecond.ToString("000");
            string FilePath = path + nombreArchivo;
            StreamWriter sw = null;
            if (!File.Exists(FilePath))
            {
                sw = File.CreateText(FilePath);
            }
            else
            {
                sw = File.AppendText(FilePath);
            }
            sw.WriteLine("Fecha: " + FechaToString + " // Nombre: " + nombre + " // tiempo: " + ts.ToString());
            sw.Close();
        }
        catch { }
    }
}
public static class dllFuncionesGenerales
{
    public static void grabarLog(MethodBase method, Exception pException, DateTime pFechaActual, params object[] values)
    {
        try
        {
            ParameterInfo[] parms = method.GetParameters();
            object[] namevalues = new object[2 * parms.Length];

            string Parameters = string.Empty;
            for (int i = 0, j = 0; i < parms.Length; i++, j += 2)
            {
                Parameters += "<" + parms[i].Name + ">";
                if (values[i].GetType() == typeof(List<cDllProductosAndCantidad>))
                {
                    List<cDllProductosAndCantidad> list = (List<cDllProductosAndCantidad>)values[i];
                    for (int y = 0; y < list.Count; y++)
                    {
                        Parameters += String.Format("codProductoNombre = {0} || cantidad = {1} || IdTransfer = {2} || isOferta = {3}", list[y].codProductoNombre, list[y].cantidad, list[y].IdTransfer, list[y].isOferta);
                    }
                }
                else
                {
                    Parameters += values[i];
                }
                Parameters += "</" + parms[i].Name + ">";
            }
            bool isNotGeneroError = cBaseDatos.spError(method.Name, Parameters, pException.Data != null ? pException.Data.ToString() : null,
                 pException.HelpLink != null ? pException.HelpLink.ToString() : null,
                 pException.InnerException != null ? pException.InnerException.ToString() : null,
                 pException.Message != null ? pException.Message.ToString() : null,
                pException.Source != null ? pException.Source.ToString() : null,
               pException.StackTrace != null ? pException.StackTrace.ToString() : null, DateTime.Now, "DLL");
            if (!isNotGeneroError)
            {
                grabarLog_Archivo(method, pException, pFechaActual, values);
            }

        }
        catch (Exception ex)
        {
        }
    }
    public static void grabarLog_Archivo(MethodBase method, Exception pException, DateTime pFechaActual, params object[] values)
    {
        try
        {
            string path = @"c:/" + @"LogWebService" + @"/";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string nombreArchivo = DateTime.Now.Year.ToString("0000") + "_" + DateTime.Now.Month.ToString("00") + "_" + DateTime.Now.Day.ToString("00") + "_h_" + DateTime.Now.Hour.ToString("00") + "_" + DateTime.Now.Minute.ToString("00") + "_" + DateTime.Now.Second.ToString("00") + "_" + DateTime.Now.Millisecond.ToString("000") + ".txt";
            string FilePath = path + nombreArchivo;
            StreamWriter sw = null;
            if (!File.Exists(FilePath))
            {
                sw = File.CreateText(FilePath);
            }
            else
            {
                sw = File.AppendText(FilePath);
                sw.WriteLine(string.Empty);
            }
            sw.WriteLine("<NombreProcedimiento>");
            sw.WriteLine(method.Name);
            //sw.WriteLine(pNombreProcedimiento);
            sw.WriteLine("</NombreProcedimiento>");

            ParameterInfo[] parms = method.GetParameters();
            object[] namevalues = new object[2 * parms.Length];

            sw.WriteLine("<Parameters>");
            for (int i = 0, j = 0; i < parms.Length; i++, j += 2)
            {
                sw.WriteLine("<" + parms[i].Name + ">");
                if (values[i].GetType() == typeof(List<cDllProductosAndCantidad>))
                {
                    List<cDllProductosAndCantidad> list = (List<cDllProductosAndCantidad>)values[i];
                    for (int y = 0; y < list.Count; y++)
                    {
                        sw.WriteLine("codProductoNombre = {0} || cantidad = {1} || IdTransfer = {2} || isOferta = {3}", list[y].codProductoNombre, list[y].cantidad, list[y].IdTransfer, list[y].isOferta);
                    }
                }
                else
                    sw.WriteLine(values[i]);
                sw.WriteLine("</" + parms[i].Name + ">");
            }
            sw.WriteLine("</Parameters>");

            sw.WriteLine(string.Empty);
            if (pException.Data != null)
            {
                sw.WriteLine("<Data>");
                sw.WriteLine(pException.Data.ToString());
                sw.WriteLine("</Data>");
            }
            if (pException.HelpLink != null)
            {
                sw.WriteLine("<HelpLink>");
                sw.WriteLine(pException.HelpLink.ToString());
                sw.WriteLine("</HelpLink>");
            }
            if (pException.InnerException != null)
            {
                sw.WriteLine("<InnerException>");
                sw.WriteLine(pException.InnerException.ToString());
                sw.WriteLine("</InnerException>");
            }
            if (pException.Message != null)
            {
                sw.WriteLine("<Message>");
                sw.WriteLine(pException.Message.ToString());
                sw.WriteLine("</Message>");
            }
            if (pException.Source != null)
            {
                sw.WriteLine("<Source>");
                sw.WriteLine(pException.Source.ToString());
                sw.WriteLine("</Source>");
            }
            if (pException.StackTrace != null)
            {
                sw.WriteLine("<StackTrace>");
                sw.WriteLine(pException.StackTrace.ToString());
                sw.WriteLine("</StackTrace>");
            }
            sw.Close();
        }
        catch (Exception ex)
        {
        }
    }
    public static dllEstadosPedido ToConvert(dkInterfaceWeb.EstadosPedido pEstado)
    {
        switch (pEstado)
        {
            case dkInterfaceWeb.EstadosPedido.Anulado:
                return dllEstadosPedido.Anulado;
            //break;
            case dkInterfaceWeb.EstadosPedido.EnPreparacion:
                return dllEstadosPedido.EnPreparacion;
            //break;
            case dkInterfaceWeb.EstadosPedido.EnSucursal:
                return dllEstadosPedido.EnSucursal;
            //break;
            case dkInterfaceWeb.EstadosPedido.Enviado:
                return dllEstadosPedido.Enviado;
            //break;
            case dkInterfaceWeb.EstadosPedido.PendienteDeFacturar:
                return dllEstadosPedido.PendienteDeFacturar;
            //break;
            case dkInterfaceWeb.EstadosPedido.Detenido:
                return dllEstadosPedido.Detenido;
            default:
                return dllEstadosPedido.Anulado;
            //    break;
        }
    }
    public static string ToConvertToString(dkInterfaceWeb.EstadosPedido pEstado)
    {
        switch (pEstado)
        {
            case dkInterfaceWeb.EstadosPedido.Anulado:
                return "Anulado";
            //break;
            case dkInterfaceWeb.EstadosPedido.EnPreparacion:
                return "En Preparacion";
            //break;
            case dkInterfaceWeb.EstadosPedido.EnSucursal:
                return "En Sucursal";
            //break;
            case dkInterfaceWeb.EstadosPedido.Enviado:
                return "Enviado";
            //break;
            case dkInterfaceWeb.EstadosPedido.PendienteDeFacturar:
                return "Pendiente De Facturar";
            //break;
            case dkInterfaceWeb.EstadosPedido.Detenido:
                return "Detenido";
            default:
                return "";
            //    break;
        }
    }
    public static cDllPedidoItem ToConvert(dkInterfaceWeb.PedidoItem pPedidoItem)
    {
        cDllPedidoItem obj = new cDllPedidoItem();
        obj.Cantidad = pPedidoItem.Cantidad;
        obj.Caracteristica = pPedidoItem.Caracteristica != null ? pPedidoItem.Caracteristica.ToString() : string.Empty;
        obj.Faltas = pPedidoItem.Faltas;
        obj.FechaIngreso = pPedidoItem.FechaIngreso.ToString() != string.Empty ? (DateTime?)(pPedidoItem.FechaIngreso) : null;
        obj.NombreObjetoComercial = pPedidoItem.NombreObjetoComercial;
        return obj;
    }
    public static cDllPedido ToConvert(dkInterfaceWeb.Pedido pPedidos)
    {
        if (pPedidos != null)
        {
            cDllPedido resultado = new cDllPedido();
            resultado.Cancelado = pPedidos.Cancelado;
            resultado.CantidadRenglones = pPedidos.CantidadRenglones;
            resultado.CantidadUnidad = pPedidos.CantidadUnidades;
            resultado.Error = pPedidos.Error;
            resultado.Estado = dllFuncionesGenerales.ToConvert(pPedidos.Estado);
            resultado.EstadoToString = dllFuncionesGenerales.ToConvertToString(pPedidos.Estado);
            resultado.Items = new List<cDllPedidoItem>();
            for (int i = 1; i <= pPedidos.Count(); i++)
            {
                resultado.Items.Add(dllFuncionesGenerales.ToConvert(pPedidos.get_Item(i)));
            }
            //resultado.FechaIngreso = pPedidos.FechaIngreso.ToString() != string.Empty ? (DateTime?)(pPedidos.FechaIngreso) : null;
            DateTime dateValue;
            resultado.FechaIngreso = DateTime.TryParse(pPedidos.FechaIngreso.ToString(), out dateValue) ? (DateTime)pPedidos.FechaIngreso : (DateTime?)null;
            resultado.FechaIngresoToString = resultado.FechaIngreso != null ? ((DateTime)resultado.FechaIngreso).ToShortDateString() : string.Empty;
            resultado.FechaIngresoHoraToString = resultado.FechaIngreso != null ? ((DateTime)resultado.FechaIngreso).ToShortTimeString() : string.Empty;

            List<cDllPedidoItem> listaPedidoItem = new List<cDllPedidoItem>();
            if (pPedidos.ItemsConProblemasDeCredito != null)
            {
                foreach (dkInterfaceWeb.PedidoItem itemPedidoItem in pPedidos.ItemsConProblemasDeCredito)
                {
                    listaPedidoItem.Add(dllFuncionesGenerales.ToConvert(itemPedidoItem));
                }
            }
            resultado.ItemsConProblemasDeCreditos = listaPedidoItem;
            resultado.Login = pPedidos.Login;
            resultado.MensajeEnFactura = pPedidos.MensajeEnFactura != null ? pPedidos.MensajeEnFactura.ToString() : string.Empty;
            resultado.MensajeEnRemito = pPedidos.MensajeEnRemito != null ? pPedidos.MensajeEnRemito.ToString() : string.Empty;
            resultado.MontoTotal = pPedidos.MontoTotal;
            resultado.NumeroFactura = pPedidos.NumeroFactura != null ? pPedidos.NumeroFactura.ToString() : string.Empty;
            resultado.NumeroRemito = pPedidos.NumeroRemito != null ? pPedidos.NumeroRemito.ToString() : string.Empty;
            resultado.DetalleSucursal = pPedidos.DetalleSucursal != null ? pPedidos.DetalleSucursal.ToString() : string.Empty;
            return resultado;
        }
        else { return null; }
    }
    public static cDllPedidoTransfer ToConvertTransfer(dkInterfaceWeb.Pedido pPedidos)
    {
        cDllPedidoTransfer resultado = new cDllPedidoTransfer();
        resultado.Cancelado = pPedidos.Cancelado;
        resultado.CantidadRenglones = pPedidos.CantidadRenglones;
        resultado.CantidadUnidad = pPedidos.CantidadUnidades;
        resultado.Error = pPedidos.Error;
        resultado.Estado = dllFuncionesGenerales.ToConvert(pPedidos.Estado);
        resultado.EstadoToString = dllFuncionesGenerales.ToConvertToString(pPedidos.Estado);
        resultado.Items = new List<cDllPedidoItem>();
        for (int i = 1; i <= pPedidos.Count(); i++)
        {
            resultado.Items.Add(dllFuncionesGenerales.ToConvert(pPedidos.get_Item(i)));
        }
        //resultado.FechaIngreso = pPedidos.FechaIngreso.ToString() != string.Empty ? (DateTime?)(pPedidos.FechaIngreso) : null;
        DateTime dateValue;
        resultado.FechaIngreso = DateTime.TryParse(pPedidos.FechaIngreso.ToString(), out dateValue) ? (DateTime)pPedidos.FechaIngreso : (DateTime?)null;
        resultado.FechaIngresoToString = resultado.FechaIngreso != null ? ((DateTime)resultado.FechaIngreso).ToShortDateString() : string.Empty;
        resultado.FechaIngresoHoraToString = resultado.FechaIngreso != null ? ((DateTime)resultado.FechaIngreso).ToShortTimeString() : string.Empty;

        List<cDllPedidoItem> listaPedidoItem = new List<cDllPedidoItem>();
        if (pPedidos.ItemsConProblemasDeCredito != null)
        {
            foreach (dkInterfaceWeb.PedidoItem itemPedidoItem in pPedidos.ItemsConProblemasDeCredito)
            {
                listaPedidoItem.Add(dllFuncionesGenerales.ToConvert(itemPedidoItem));
            }
        }
        resultado.ItemsConProblemasDeCreditos = listaPedidoItem;
        resultado.Login = pPedidos.Login;
        resultado.MensajeEnFactura = pPedidos.MensajeEnFactura != null ? pPedidos.MensajeEnFactura.ToString() : string.Empty;
        resultado.MensajeEnRemito = pPedidos.MensajeEnRemito != null ? pPedidos.MensajeEnRemito.ToString() : string.Empty;
        resultado.MontoTotal = pPedidos.MontoTotal;
        resultado.NumeroFactura = pPedidos.NumeroFactura != null ? pPedidos.NumeroFactura.ToString() : string.Empty;
        resultado.NumeroRemito = pPedidos.NumeroRemito != null ? pPedidos.NumeroRemito.ToString() : string.Empty;
        return resultado;
    }
    public static dllTipoComprobante ToConvert(dkInterfaceWeb.TiposComprobante pEstado)
    {
        switch (pEstado)
        {
            case dkInterfaceWeb.TiposComprobante.CIE:
                return dllTipoComprobante.CIE;
            case dkInterfaceWeb.TiposComprobante.FAC:
                return dllTipoComprobante.FAC;
            case dkInterfaceWeb.TiposComprobante.NCI:
                return dllTipoComprobante.NCI;
            case dkInterfaceWeb.TiposComprobante.NCR:
                return dllTipoComprobante.NCR;
            case dkInterfaceWeb.TiposComprobante.NDE:
                return dllTipoComprobante.NDE;
            case dkInterfaceWeb.TiposComprobante.NDI:
                return dllTipoComprobante.NDI;
            case dkInterfaceWeb.TiposComprobante.REC:
                return dllTipoComprobante.REC;
            case dkInterfaceWeb.TiposComprobante.RES:
                return dllTipoComprobante.RES;
            case dkInterfaceWeb.TiposComprobante.OSC:
                return dllTipoComprobante.OSC;
            default:
                return dllTipoComprobante.NN;
            //    break;
        }
    }

    //public static string ToConvertToString(string pValue)
    //{
    //    switch (Convert.ToInt32(pValue))
    //    {
    //        case 0:
    //            return "FAC";
    //        case 1:
    //            return "REC";
    //        case 2:
    //            return "NDE";
    //        case 3:
    //            return "NCR";
    //        case 4:
    //            return "NDI";
    //        case 5:
    //            return "NCI";
    //        case 9:
    //            return "RES";
    //        case 10:
    //            return "CIE";
    //        case 13:
    //            return "OSC";
    //        default:
    //            return "";
    //        //FAC = 0,
    //        //REC = 1,
    //        //NDE = 2,
    //        //NCR = 3,
    //        //NDI = 4,
    //        //NCI = 5,
    //        //RES = 9,
    //        //CIE = 10,
    //        //OSC = 13,
    //    }
    //}
    public static string ToConvertToString(dkInterfaceWeb.TiposComprobante pEstado)
    {
        switch (pEstado)
        {
            case dkInterfaceWeb.TiposComprobante.CIE:
                return "CIE";
            case dkInterfaceWeb.TiposComprobante.FAC:
                return "FAC";
            case dkInterfaceWeb.TiposComprobante.NCI:
                return "NCI";
            case dkInterfaceWeb.TiposComprobante.NCR:
                return "NCR";
            case dkInterfaceWeb.TiposComprobante.NDE:
                return "NDE";
            case dkInterfaceWeb.TiposComprobante.NDI:
                return "NDI";
            case dkInterfaceWeb.TiposComprobante.REC:
                return "REC";
            case dkInterfaceWeb.TiposComprobante.RES:
                return "RES";
            case dkInterfaceWeb.TiposComprobante.OSC:
                return "OSC";
            default:
                return "";

        }
    }

    public static cResumen ToConvert(dkInterfaceWeb.Resumen pResumen)
    {
        DateTime dateValue;
        cResumen obj = new cResumen();
        obj.Numero = pResumen.Numero;
        obj.NumeroSemana = pResumen.NumeroSemana != null ? pResumen.NumeroSemana.ToString() : string.Empty;
        obj.PeriodoDesde = DateTime.TryParse(pResumen.PeriodoDesde.ToString(), out dateValue) ? (DateTime)pResumen.PeriodoDesde : (DateTime?)null;
        obj.PeriodoDesdeToString = obj.PeriodoDesde != null ? ((DateTime)obj.PeriodoDesde).ToShortDateString() : string.Empty;
        obj.PeriodoHasta = DateTime.TryParse(pResumen.PeriodoHasta.ToString(), out dateValue) ? (DateTime)pResumen.PeriodoHasta : (DateTime?)null;
        obj.PeriodoHastaToString = obj.PeriodoHasta != null ? ((DateTime)obj.PeriodoHasta).ToShortDateString() : string.Empty;
        obj.TotalResumen = pResumen.TotalResumen;
        return obj;
    }
    public static cResumenDetalle ToConvert(dkInterfaceWeb.ResumenItem pResumenDetalle)
    {
        cResumenDetalle obj = new cResumenDetalle();
        obj.Descripcion = pResumenDetalle.Descripcion != null ? pResumenDetalle.Descripcion.ToString() : string.Empty;
        obj.Dia = pResumenDetalle.Dia != null ? pResumenDetalle.Dia.ToString() : string.Empty;
        obj.Importe = pResumenDetalle.Importe != null ? pResumenDetalle.Importe.ToString() : string.Empty;
        obj.NumeroHoja = pResumenDetalle.NumeroHoja != null ? pResumenDetalle.NumeroHoja.ToString() : string.Empty;
        obj.NumeroItem = pResumenDetalle.NumeroItem;
        obj.NumeroResumen = pResumenDetalle.NumeroResumen != null ? pResumenDetalle.NumeroResumen.ToString() : string.Empty;
        obj.TipoComprobante = (pResumenDetalle.TipoComprobante != null && !string.IsNullOrEmpty(pResumenDetalle.TipoComprobante.ToString())) 
            ? 
            Enum.Parse(typeof(dkInterfaceWeb.TiposComprobante), pResumenDetalle.TipoComprobante.ToString()).ToString():
            string.Empty;
        return obj;
    }

    public static cVencimientoResumen ToConvert(dkInterfaceWeb.VtoResumenPorFecha pVto)
    {
        DateTime dateValue;
        cVencimientoResumen obj = new cVencimientoResumen();
        obj.Tipo = pVto.Tipo;
        obj.NumeroComprobante = pVto.NumeroComprobante;
        obj.Fecha = DateTime.TryParse(pVto.Fecha.ToString(), out dateValue) ? (DateTime)pVto.Fecha : (DateTime?)null;
        obj.FechaToString = obj.Fecha != null ? ((DateTime)obj.Fecha).ToShortDateString() : "";
        obj.FechaVencimiento = DateTime.TryParse(pVto.FechaVencimiento.ToString(), out dateValue) ? (DateTime)pVto.FechaVencimiento : (DateTime?)null;
        obj.FechaVencimientoToString = obj.FechaVencimiento != null ? ((DateTime)obj.FechaVencimiento).ToShortDateString() : "";
        obj.Importe = pVto.Importe;
        return obj;
    }

    public static cNotaDeDebito ConvertToNotaDeDebito(dkInterfaceWeb.NotaDeDebito pObjNotaDeDebito)
    {
        cNotaDeDebito resultado = null;
        if (pObjNotaDeDebito != null)
        {
            resultado = new cNotaDeDebito();
            resultado.CantidadHojas = pObjNotaDeDebito.CantidadHojas;
            resultado.Destinatario = pObjNotaDeDebito.Destinatario;
            DateTime dateValue;
            resultado.Fecha = DateTime.TryParse(pObjNotaDeDebito.Fecha.ToString(), out dateValue) ? (DateTime)pObjNotaDeDebito.Fecha : (DateTime?)null;
            resultado.FechaToString = resultado.Fecha != null ? ((DateTime)resultado.Fecha).ToShortDateString() : string.Empty;
            try
            {
                if (pObjNotaDeDebito.Numero[0].ToString() != "B")
                {
                    resultado.MontoExento = pObjNotaDeDebito.MontoExento;
                    resultado.MontoGravado = pObjNotaDeDebito.MontoGravado;
                    resultado.MontoIvaInscripto = pObjNotaDeDebito.MontoIvaInscripto;
                    resultado.MontoIvaNoInscripto = pObjNotaDeDebito.MontoIvaNoInscripto;
                }
            }
            catch { }
            resultado.MontoPercepcionDGR = pObjNotaDeDebito.MontoPercepcionDGR;
            resultado.MontoTotal = pObjNotaDeDebito.MontoTotal;
            resultado.Motivo = pObjNotaDeDebito.Motivo;
            resultado.Numero = pObjNotaDeDebito.Numero;
        }
        return resultado;
    }
    public static cNotaDeCredito ConvertToNotaDeCredito(dkInterfaceWeb.NotaDeCredito pObjNotaDeCredito)
    {
        cNotaDeCredito resultado = null;
        if (pObjNotaDeCredito != null)
        {
            resultado = new cNotaDeCredito();
            resultado.CantidadHojas = pObjNotaDeCredito.CantidadHojas;
            resultado.Destinatario = pObjNotaDeCredito.Destinatario;
            DateTime dateValue;
            resultado.Fecha = DateTime.TryParse(pObjNotaDeCredito.Fecha.ToString(), out dateValue) ? (DateTime)pObjNotaDeCredito.Fecha : (DateTime?)null;
            resultado.FechaToString = resultado.Fecha != null ? ((DateTime)resultado.Fecha).ToShortDateString() : string.Empty;
            try
            {
                if (pObjNotaDeCredito.Numero[0].ToString() != "B")
                {
                    resultado.MontoExento = pObjNotaDeCredito.MontoExento;
                    resultado.MontoGravado = pObjNotaDeCredito.MontoGravado;
                    resultado.MontoIvaInscripto = pObjNotaDeCredito.MontoIvaInscripto;
                    resultado.MontoIvaNoInscripto = pObjNotaDeCredito.MontoIvaNoInscripto;
                }
            }
            catch { }
            resultado.MontoPercepcionDGR = pObjNotaDeCredito.MontoPercepcionDGR;
            resultado.MontoTotal = pObjNotaDeCredito.MontoTotal;
            resultado.Motivo = pObjNotaDeCredito.Motivo;
            resultado.Numero = pObjNotaDeCredito.Numero;
            resultado.TotalUnidades = pObjNotaDeCredito.TotalUnidades;
        }
        return resultado;
    }
    public static cFactura ConvertToFactura(dkInterfaceWeb.Factura pObjFactura)
    {
        cFactura resultado = null;
        if (pObjFactura != null)
        {
            resultado = new cFactura();
            resultado.CantidadHojas = pObjFactura.CantidadHojas;
            resultado.CantidadRenglones = pObjFactura.CantidadRenglones;
            resultado.CodigoFormaDePago = pObjFactura.CodigoFormaDePago;
            resultado.DescuentoEspecial = pObjFactura.DescuentoEspecial;
            resultado.DescuentoNetos = pObjFactura.DescuentoNetos;
            resultado.DescuentoPerfumeria = pObjFactura.DescuentoPerfumeria;
            resultado.DescuentoWeb = pObjFactura.DescuentoWeb;
            resultado.Destinatario = pObjFactura.Destinatario;
            resultado.Fecha = pObjFactura.Fecha;
            DateTime dateValue;
            resultado.Fecha = DateTime.TryParse(pObjFactura.Fecha.ToString(), out dateValue) ? (DateTime)pObjFactura.Fecha : (DateTime?)null;
            resultado.FechaToString = resultado.Fecha != null ? ((DateTime)resultado.Fecha).ToShortDateString() : string.Empty;
            try
            {
                if (pObjFactura.Numero[0].ToString() != "B")
                {
                    resultado.MontoExento = pObjFactura.MontoExento;
                    resultado.MontoGravado = pObjFactura.MontoGravado;
                    resultado.MontoIvaInscripto = pObjFactura.MontoIvaInscripto;
                    resultado.MontoIvaNoInscripto = pObjFactura.MontoIvaNoInscripto;
                }
            }
            catch { }
            resultado.MontoPercepcionDGR = pObjFactura.MontoPercepcionDGR;
            resultado.MontoTotal = pObjFactura.MontoTotal;
            resultado.Numero = pObjFactura.Numero;
            resultado.NumeroCuentaCorriente = pObjFactura.NumeroCuentaCorriente;
            resultado.NumeroRemito = pObjFactura.NumeroRemito;
            resultado.TotalUnidades = pObjFactura.TotalUnidades;
            resultado.MontoPercepcionMunicipal = pObjFactura.MontoPercepcionMunicipal;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWebFacturaTrazable = new dkInterfaceWeb.ServiciosWEB();
                resultado.FacturaTrazable = objServWebFacturaTrazable.FacturaTrazable(pObjFactura.Numero);
            }
            catch //(Exception exFacturaTrazable)
            {
            }

        }
        return resultado;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pEstado"></param>
    /// <returns></returns>
    public static dllEstadoCheque ToConvert(dkInterfaceWeb.EstadoCheque pEstado)
    {
        switch (pEstado)
        {
            case dkInterfaceWeb.EstadoCheque.Aceptado:
                return dllEstadoCheque.Aceptado;
            case dkInterfaceWeb.EstadoCheque.Cambiado:
                return dllEstadoCheque.Cambiado;
            case dkInterfaceWeb.EstadoCheque.Depositado:
                return dllEstadoCheque.Depositado;
            case dkInterfaceWeb.EstadoCheque.EnCartera:
                return dllEstadoCheque.EnCartera;
            case dkInterfaceWeb.EstadoCheque.Rechazado:
                return dllEstadoCheque.Rechazado;
            case dkInterfaceWeb.EstadoCheque.Retirado:
                return dllEstadoCheque.Retirado;
            default:
                return dllEstadoCheque.Aceptado;
        }
    }
    public static string ToConvertToString(dkInterfaceWeb.EstadoCheque pEstado)
    {
        switch (pEstado)
        {
            case dkInterfaceWeb.EstadoCheque.Aceptado:
                return "Aceptado";
            //break;
            case dkInterfaceWeb.EstadoCheque.Cambiado:
                return "Cambiado";
            //break;
            case dkInterfaceWeb.EstadoCheque.Depositado:
                return "Depositado";
            //break;
            case dkInterfaceWeb.EstadoCheque.EnCartera:
                return "EnCartera";
            //break;
            case dkInterfaceWeb.EstadoCheque.Rechazado:
                return "Rechazado";
            //break;
            case dkInterfaceWeb.EstadoCheque.Retirado:
                return "Retirado";
            default:
                return "";
            //    break;
        }
    }
    public static cDllChequeRecibido ConvertToChequeRecibido(dkInterfaceWeb.ChequeRecibido pChequeRecibido)
    {
        cDllChequeRecibido resultado = null;
        if (pChequeRecibido != null)
        {
            resultado = new cDllChequeRecibido();
            DateTime dateValue;
            resultado.Banco = pChequeRecibido.Banco;
            resultado.Estado = ToConvert(pChequeRecibido.Estado);
            resultado.EstadoToString = ToConvertToString(pChequeRecibido.Estado);
            resultado.Fecha = pChequeRecibido.Fecha != null ? pChequeRecibido.Fecha.ToString() : string.Empty;
            resultado.FechaVencimiento = DateTime.TryParse(pChequeRecibido.FechaVencimiento.ToString(), out dateValue) ? (DateTime)pChequeRecibido.FechaVencimiento : (DateTime?)null;
            resultado.FechaVencimientoToString = resultado.FechaVencimiento != null ? ((DateTime)resultado.FechaVencimiento).ToShortDateString() : string.Empty;
            resultado.Importe = pChequeRecibido.Importe;
            resultado.Numero = pChequeRecibido.Numero != null ? pChequeRecibido.Numero.ToString() : string.Empty;
        }
        return resultado;
    }
    public static cPlan ToConvert(dkInterfaceWeb.Plan pPlanItem)
    {
        cPlan obj = null;
        if (pPlanItem != null)
        {
            obj = new cPlan();
            obj.Nombre = pPlanItem.Nombre;
            obj.PideSemana = pPlanItem.PideSemana;
        }
        return obj;
    }
    public static cPlanillaObSoc ToConvert(dkInterfaceWeb.PlanillaObSoc pPlanillaObSoc)
    {
        cPlanillaObSoc obj = null;
        if (pPlanillaObSoc != null)
        {
            obj = new cPlanillaObSoc();
            obj.Anio = pPlanillaObSoc.Anio == null ? string.Empty : Convert.ToString(pPlanillaObSoc.Anio);
            obj.Fecha = pPlanillaObSoc.Fecha;
            obj.FechaToString = pPlanillaObSoc.Fecha != null ? ((DateTime)pPlanillaObSoc.Fecha).ToShortDateString() : string.Empty;
            obj.Importe = pPlanillaObSoc.Importe;
            obj.Mes = pPlanillaObSoc.Mes == null ? string.Empty : Convert.ToString(pPlanillaObSoc.Mes);
            obj.Quincena = pPlanillaObSoc.Quincena == null ? string.Empty : Convert.ToString(pPlanillaObSoc.Quincena);
            obj.Semana = pPlanillaObSoc.Semana == null ? string.Empty : Convert.ToString(pPlanillaObSoc.Semana);
        }
        return obj;
    }
    public static cObraSocialCliente ToConvert(dkInterfaceWeb.ObraSocialCliente pObraSocialCliente)
    {
        cObraSocialCliente obj = null;
        if (pObraSocialCliente != null)
        {
            obj = new cObraSocialCliente();
            obj.CantidadHojas = pObraSocialCliente.CantidadHojas == null ? string.Empty : Convert.ToString(pObraSocialCliente.CantidadHojas);
            obj.Destinatario = pObraSocialCliente.Destinatario == null ? string.Empty : Convert.ToString(pObraSocialCliente.Destinatario);
            obj.Fecha = pObraSocialCliente.Fecha;
            obj.FechaToString = pObraSocialCliente.Fecha != null ? ((DateTime)pObraSocialCliente.Fecha).ToShortDateString() : string.Empty;
            obj.MontoTotal = pObraSocialCliente.MontoTotal;
            obj.NombrePlan = pObraSocialCliente.NombrePlan == null ? string.Empty : Convert.ToString(pObraSocialCliente.NombrePlan);
            obj.NumeroPlanilla = pObraSocialCliente.NumeroPlanilla;
        }
        return obj;
    }

    public static cObraSocialClienteItem ToConvert(dkInterfaceWeb.ObraSocialClienteItem pObraSocialClienteItem)
    {
        cObraSocialClienteItem obj = null;
        if (pObraSocialClienteItem != null)
        {
            obj = new cObraSocialClienteItem();
            obj.Descripcion = pObraSocialClienteItem.Descripcion == null ? string.Empty : Convert.ToString(pObraSocialClienteItem.Descripcion);
            obj.Importe = pObraSocialClienteItem.Importe == null ? string.Empty : Convert.ToString(pObraSocialClienteItem.Importe);
            obj.NumeroHoja = pObraSocialClienteItem.NumeroHoja == null ? string.Empty : Convert.ToString(pObraSocialClienteItem.NumeroHoja);
            obj.NumeroItem = pObraSocialClienteItem.NumeroItem;
            obj.NumeroObraSocialCliente = pObraSocialClienteItem.NumeroObraSocialCliente == null ? string.Empty : Convert.ToString(pObraSocialClienteItem.NumeroObraSocialCliente);
        }
        return obj;
    }
    public static cCbteParaImprimir ToConvert(dkInterfaceWeb.CbteParaImprimir pValor)
    {
        cCbteParaImprimir obj = null;
        if (pValor != null)
        {
            obj = new cCbteParaImprimir();
            obj.FechaComprobante = pValor.FechaComprobante;
            obj.FechaComprobanteToString = pValor.FechaComprobante != null ? ((DateTime)pValor.FechaComprobante).ToShortDateString() : string.Empty;
            obj.NumeroComprobante = pValor.NumeroComprobante == null ? string.Empty : Convert.ToString(pValor.NumeroComprobante);
            obj.TipoComprobante = pValor.TipoComprobante == null ? string.Empty : Convert.ToString(pValor.TipoComprobante);
        }
        return obj;
    }
    public static cConsObraSocial ToConvert(dkInterfaceWeb.ConsObraSocial pValor)
    {
        cConsObraSocial obj = null;
        if (pValor != null)
        {
            obj = new cConsObraSocial();
            obj.FechaComprobante = pValor.FechaComprobante;
            obj.FechaComprobanteToString = pValor.FechaComprobante != null ? ((DateTime)pValor.FechaComprobante).ToShortDateString() : string.Empty;
            obj.Detalle = pValor.Detalle == null ? string.Empty : Convert.ToString(pValor.Detalle);
            obj.TipoComprobante = pValor.TipoComprobante == null ? string.Empty : Convert.ToString(pValor.TipoComprobante);
            obj.NumeroComprobante = pValor.NumeroComprobante == null ? string.Empty : Convert.ToString(pValor.NumeroComprobante);
            obj.Importe = pValor.Importe;//== null ? string.Empty : Convert.ToString(pValor.Importe);
        }
        return obj;
    }
    public static cLote ConvertToLote(dkInterfaceWeb.Lote pObjLote)
    {
        cLote resultado = null;
        if (pObjLote != null)
        {
            resultado = new cLote();
            resultado.ID = pObjLote.ID;
            resultado.NombreProducto = pObjLote.NombreProducto;
            resultado.NumeroLote = pObjLote.NumeroLote;
            resultado.FechaVencimiento = pObjLote.FechaVencimiento;
            DateTime dateValue;
            resultado.FechaVencimiento = DateTime.TryParse(pObjLote.FechaVencimiento.ToString(), out dateValue) ? (DateTime)pObjLote.FechaVencimiento : (DateTime?)null;
            resultado.FechaVencimientoToString = resultado.FechaVencimiento != null ? ((DateTime)resultado.FechaVencimiento).ToShortDateString() : string.Empty;
        }
        return resultado;
    }
    public static cDevolucionItemPrecarga ConvertToItemSolicitudDevCliente(dkInterfaceWeb.SolicitudDevCliente pObjSDC)
    {
        cDevolucionItemPrecarga resultado = null;
        if (pObjSDC != null)
        {
            resultado = new cDevolucionItemPrecarga();
            resultado.dev_numeroitem = pObjSDC.NumeroItem;
            resultado.dev_numerocliente = pObjSDC.NumeroCliente;
            resultado.dev_numerofactura = pObjSDC.NumeroFactura;
            resultado.dev_numerosolicituddevolucion = pObjSDC.NumeroSolicitud;
            resultado.dev_nombreproductodevolucion = pObjSDC.NombreProductoDevolucion;
            resultado.dev_fecha = pObjSDC.Fecha;
            DateTime dateValue;
            resultado.dev_fecha = DateTime.TryParse(pObjSDC.Fecha.ToString(), out dateValue) ? (DateTime)pObjSDC.Fecha : DateTime.Now;
            resultado.dev_fechaToString = resultado.dev_fecha!= null ? ((DateTime)resultado.dev_fecha).ToShortDateString() : string.Empty;
            switch (pObjSDC.Motivo) { 
                case dkInterfaceWeb.MotivoDevolucion.BienFacturadoMalEnviado:
                    resultado.dev_motivo = dllMotivoDevolucion.BienFacturadoMalEnviado;
                    break;
                case dkInterfaceWeb.MotivoDevolucion.ProductoMalEstado:
                    resultado.dev_motivo = dllMotivoDevolucion.ProductoMalEstado;
                    break;
                case dkInterfaceWeb.MotivoDevolucion.FacturadoNoPedido:
                    resultado.dev_motivo = dllMotivoDevolucion.FacturadoNoPedido;
                    break;
                case dkInterfaceWeb.MotivoDevolucion.ProductoDeMasSinSerFacturado:
                    resultado.dev_motivo = dllMotivoDevolucion.ProductoDeMasSinSerFacturado;
                    break;
                case dkInterfaceWeb.MotivoDevolucion.VencimientoCorto:
                    resultado.dev_motivo = dllMotivoDevolucion.VencimientoCorto;
                    break;
                case dkInterfaceWeb.MotivoDevolucion.ProductoFallaFabricante:
                    resultado.dev_motivo = dllMotivoDevolucion.ProductoFallaFabricante;
                    break;
                case dkInterfaceWeb.MotivoDevolucion.Vencido:
                    resultado.dev_motivo = dllMotivoDevolucion.Vencido;
                    break;
            }
            resultado.dev_numeroitemfactura = pObjSDC.NumeroItemFactura;
            resultado.dev_nombreproductofactura = pObjSDC.NombreProductoFactura;
            resultado.dev_cantidad = pObjSDC.Cantidad;
            resultado.dev_numerolote = pObjSDC.NumeroLote;
            resultado.dev_fechavencimientolote = pObjSDC.FechaVencimiento;
            resultado.dev_fechavencimientolote = DateTime.TryParse(pObjSDC.FechaVencimiento.ToString(), out dateValue) ? (DateTime)pObjSDC.FechaVencimiento : DateTime.Now;
            resultado.dev_fechavencimientoloteToString = resultado.dev_fechavencimientolote != null ? ((DateTime)resultado.dev_fechavencimientolote).ToShortDateString() : string.Empty;
            resultado.dev_estado = pObjSDC.Estado;
            resultado.dev_mensaje = pObjSDC.MensajeRechazo;
            resultado.dev_cantidadrecibida = pObjSDC.CantidadRecibida;
            resultado.dev_cantidadrechazada = pObjSDC.CantidadRechazada;
            resultado.dev_idsucursal = pObjSDC.IDSucursal;
            resultado.dev_numerosolicitudNC = pObjSDC.NumeroSolicitudNC;
        }
        return resultado;
    }
    public static dkInterfaceWeb.SolicitudDevCliente ConvertFromItemSolicitudDevCliente(cDevolucionItemPrecarga pObjSDC)
    {
        dkInterfaceWeb.SolicitudDevCliente resultado = null;
        if (pObjSDC != null)
        {
            resultado = new dkInterfaceWeb.SolicitudDevCliente();
            resultado.NumeroItem = pObjSDC.dev_numeroitem;
            resultado.NumeroCliente = pObjSDC.dev_numerocliente;
            resultado.NumeroFactura = pObjSDC.dev_numerofactura;
            resultado.NumeroSolicitud = pObjSDC.dev_numerosolicituddevolucion;
            resultado.NombreProductoDevolucion = pObjSDC.dev_nombreproductodevolucion;
            resultado.Fecha = pObjSDC.dev_fecha;
            DateTime dateValue;
            resultado.Fecha = DateTime.TryParse(pObjSDC.dev_fecha.ToString(), out dateValue) ? (DateTime)pObjSDC.dev_fecha : DateTime.Now;

            switch (pObjSDC.dev_motivo) { 
                case dllMotivoDevolucion.BienFacturadoMalEnviado:
                    resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.BienFacturadoMalEnviado;
                    break;
                case dllMotivoDevolucion.ProductoMalEstado:
                    resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.ProductoMalEstado;
                    break;
                case dllMotivoDevolucion.FacturadoNoPedido:
                    resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.FacturadoNoPedido;
                    break;
                case dllMotivoDevolucion.ProductoDeMasSinSerFacturado:
                    resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.ProductoDeMasSinSerFacturado;
                    break;
                case dllMotivoDevolucion.VencimientoCorto:
                    resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.VencimientoCorto;
                    break;
                case dllMotivoDevolucion.ProductoFallaFabricante:
                    resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.ProductoFallaFabricante;
                    break;
                case dllMotivoDevolucion.Vencido:
                    resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.Vencido;
                    break;
            }
            resultado.NumeroItemFactura = pObjSDC.dev_numeroitemfactura;
            resultado.NombreProductoFactura = pObjSDC.dev_nombreproductofactura;
            resultado.Cantidad = pObjSDC.dev_cantidad;
            resultado.NumeroLote = pObjSDC.dev_numerolote;
            resultado.FechaVencimiento = pObjSDC.dev_fechavencimientolote;
            resultado.FechaVencimiento = Convert.ToDateTime(pObjSDC.dev_fechavencimientoloteToString);
            //resultado.FechaVencimiento = DateTime.TryParse(pObjSDC.dev_fechavencimientolote.ToString(), out dateValue) ? (DateTime)pObjSDC.dev_fechavencimientolote : DateTime.Now;
            resultado.Estado = pObjSDC.dev_estado;
            resultado.MensajeRechazo = pObjSDC.dev_mensaje;
            resultado.CantidadRecibida = pObjSDC.dev_cantidadrecibida;
            resultado.CantidadRechazada = pObjSDC.dev_cantidadrechazada;
            resultado.IDSucursal = pObjSDC.dev_idsucursal;
            resultado.NumeroSolicitudNC = pObjSDC.dev_numerosolicitudNC;
        }
        return resultado;
    }
}
