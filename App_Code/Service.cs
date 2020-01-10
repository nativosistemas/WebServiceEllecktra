using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Reflection;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public static Autenticacion CredencialAutenticacion;

    public static Boolean VerificarPermisos(Autenticacion pValor)
    {
        if (pValor == null)
        {
            return false;
        }
        else
        {
            //Verifica los permiso Ej. Consulta a BD 
            if (pValor.UsuarioNombre == System.Configuration.ConfigurationManager.AppSettings["ws_usu"] && pValor.UsuarioClave == System.Configuration.ConfigurationManager.AppSettings["ws_psw"])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    [WebMethod]
    public void Login(string pNombre, string pContraseña)
    {
        CredencialAutenticacion = new Autenticacion();
        CredencialAutenticacion.UsuarioNombre = pNombre;
        CredencialAutenticacion.UsuarioClave = pContraseña;
    }
    //[WebMethod]
    //public void ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas_Prueba()
    //{
    //    List<cComprobantesDiscriminadosDePuntoDeVenta> lista = ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas("romanello", DateTime.Now.AddDays(-5), DateTime.Now);
    //    var tt = 0;
    //}
    [WebMethod]
    public List<cComprobantesDiscriminadosDePuntoDeVenta> ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas(string pIdentificadorCliente, DateTime pFechaDesde, DateTime pFechaHasta)
    {
        List<cComprobantesDiscriminadosDePuntoDeVenta> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
            try
            {
                resultado = new List<cComprobantesDiscriminadosDePuntoDeVenta>();
                dkInterfaceWeb._ComprobDiscriminadoCOL objLista = objServWeb.ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas(pIdentificadorCliente, pFechaDesde, pFechaHasta);
                if (objLista != null)
                {
                    for (int i = 1; i <= objLista.Count(); i++)
                    { //objLista.get_Item(i).
                        cComprobantesDiscriminadosDePuntoDeVenta obj = new cComprobantesDiscriminadosDePuntoDeVenta();
                        obj.Comprobante = objLista.get_Item(i).Comprobante;
                        obj.Destinatario = objLista.get_Item(i).Destinatario;//string
                        //obj.Fecha = objLista.get_Item(i).Fecha == null ? (DateTime?)null : (DateTime)objLista.get_Item(i).Fecha;
                        //obj.FechaToString = objLista.get_Item(i).Fecha == null ? string.Empty : ((DateTime)objLista.get_Item(i).Fecha).ToShortDateString(); //DateTime
                        obj.Fecha = (DateTime)objLista.get_Item(i).Fecha;//DateTime
                        obj.FechaToString = ((DateTime)objLista.get_Item(i).Fecha).ToString(); //string
                        obj.MontoExento = objLista.get_Item(i).MontoExento;//decimal
                        obj.MontoGravado = objLista.get_Item(i).MontoGravado;//decimal
                        obj.MontoIvaInscripto = objLista.get_Item(i).MontoIvaInscripto;//decimal
                        obj.MontoIvaNoInscripto = objLista.get_Item(i).MontoIvaNoInscripto;//decimal
                        obj.MontoPercepcionesDGR = objLista.get_Item(i).MontoPercepcionesDGR;//decimal
                        obj.MontoTotal = objLista.get_Item(i).MontoTotal;//decimal
                        obj.NumeroComprobante = objLista.get_Item(i).NumeroComprobante;//string
                        obj.MontoPercepcionesMunicipal = objLista.get_Item(i).MontoPercepcionesMunicipal;//decimal
                        resultado.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdentificadorCliente, pFechaDesde, pFechaHasta);
                resultado = null;
            }
        }
        return resultado;
    }

    [WebMethod]
    public List<string> ObtenerTiposDeComprobantesAMostrar(string pIdentificadorCliente)
    {
        List<string> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
            resultado = new List<string>();
            try
            {
                // var lista = objServWeb.ObtenerTiposDeComprobantesAMostrar(pIdentificadorCliente);
                VBA._Collection lista = objServWeb.ObtenerTiposDeComprobantesAMostrar(pIdentificadorCliente);
                foreach (var item in lista)
                {
                    resultado.Add((string)item);
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdentificadorCliente);
                resultado = null;
            }
        }
        return resultado;
    }
    [WebMethod]
    public void ModificarPasswordWEB(string pIdentificadorCliente, string pPassActual, string pPassNueva)
    {
        //decimal resultado = 0;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
            try
            {
                objServWeb.ModificarPasswordWEB(pIdentificadorCliente, pPassActual, pPassNueva);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdentificadorCliente, pPassActual, pPassNueva);
            }
            //resultado = objServWeb.ObtenerSaldoTotal(pIdentificadorCliente);
        }
        //return resultado;
    }
    [WebMethod]
    public decimal ObtenerSaldoTotal(string pIdentificadorCliente)
    {
        decimal resultado = 0;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.ObtenerSaldoTotal(pIdentificadorCliente);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdentificadorCliente);
            }
        }
        return resultado;
    }
    [WebMethod]
    public cDllPedido TomarPedido(string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto, bool pIsUrgente)
    {
        cDllPedido ResultadoFinal = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("TomarPedido");
            //ResultadoFinal = new cDllPedido();
            try
            {
                dkInterfaceWeb.Pedido Resultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.Pedido pedido = new dkInterfaceWeb.Pedido();

                // llenar datos pedidos
                pedido.Login = pLoginCliente;
                pedido.MensajeEnFactura = pMensajeEnFactura;
                pedido.MensajeEnRemito = pMensajeEnRemito;

                //List<cDllProductosAndCantidad> ListaProducto = Serializador.DeserializarJson<List<cDllProductosAndCantidad>>(pListaProducto);
                // Cargar productos al carrito
                foreach (cDllProductosAndCantidad item in pListaProducto)
                {
                    pedido.Add(item.codProductoNombre, item.cantidad, item.isOferta ? "$" : " ");
                }
                dkInterfaceWeb.TipoEnvio tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
                switch (pTipoEnvio)
                {
                    case "E":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Encomienda;
                        break;
                    case "R":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
                        break;
                    case "C":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Cadeteria;
                        break;
                    case "M":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Mostrador;
                        break;
                    //default:
                    //    break;
                }

                Resultado = objServWeb.TomarPedido(pedido, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL", pIsUrgente);
                if (Resultado != null)
                {
                    ResultadoFinal = dllFuncionesGenerales.ToConvert(Resultado);
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pIsUrgente);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return ResultadoFinal;
    }


    //[WebMethod]
    //public string ObtenerNotaDeDebito_Prueba()
    //{
    //    string resultado = "Ok";
    //    var lla = ObtenerNotaDeDebito("A000100166654", "romanello");
    //    return resultado;
    //}
    [WebMethod]
    public cNotaDeDebito ObtenerNotaDeDebito(string pNumeroNotaDeDebito, string pLoginWeb)
    {
        cNotaDeDebito resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerNotaDeDebito");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.NotaDeDebito objNotaDeDebito = objServWeb.ObtenerNotaDeDebito(pNumeroNotaDeDebito, pLoginWeb);
                if (objNotaDeDebito != null)
                {
                    resultado = dllFuncionesGenerales.ConvertToNotaDeDebito(objNotaDeDebito);
                    resultado.lista = ObtenerDetalleNotaDeDebito(pNumeroNotaDeDebito);
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroNotaDeDebito, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    public List<cNotaDeDebitoDetalle> ObtenerDetalleNotaDeDebito(string pNumeroNotaDeDebito)
    {
        List<cNotaDeDebitoDetalle> resultado = null;
        try
        {
            dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
            dkInterfaceWeb.NotaDeDebitoItemCOL objListaDetalle = objServWeb.ObtenerItemsDeNotaDeDebito(pNumeroNotaDeDebito);
            if (objListaDetalle != null)
            {
                resultado = new List<cNotaDeDebitoDetalle>();
                for (int i = 1; i <= objListaDetalle.Count(); i++)
                {
                    cNotaDeDebitoDetalle _objDetalleNotaDeDebito = new cNotaDeDebitoDetalle();
                    _objDetalleNotaDeDebito.Descripcion = objListaDetalle.get_Item(i).Descripcion;
                    _objDetalleNotaDeDebito.Importe = objListaDetalle.get_Item(i).Importe == null ? "" : objListaDetalle.get_Item(i).Importe.ToString();
                    _objDetalleNotaDeDebito.NumeroHoja = objListaDetalle.get_Item(i).NumeroHoja;
                    _objDetalleNotaDeDebito.NumeroItem = objListaDetalle.get_Item(i).NumeroItem;
                    _objDetalleNotaDeDebito.NumeroNotaDeDebito = objListaDetalle.get_Item(i).NumeroNotaDeDebito;
                    resultado.Add(_objDetalleNotaDeDebito);
                }
            }
        }
        catch (Exception ex)
        {
            dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroNotaDeDebito);
        }
        return resultado;
    }
    //[WebMethod]
    //public string ObtenerNotaDeCredito_Prueba()
    //{
    //    // A000103173025
    //    //A000103188526
    //    //03182431
    //    dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
    //    //dkInterfaceWeb.NotaDeCreditoItemCOL objListaDetalle = objServWeb.ObtenerItemsDeNotaDeCredito("A000103182431");
    //    var dfd = ObtenerNotaDeCredito("B000100148718", "HARLEY");
    //    return "OK";
    //}
    [WebMethod]
    public cNotaDeCredito ObtenerNotaDeCredito(string pNumeroNotaDeCredito, string pLoginWeb)
    {
        cNotaDeCredito resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerNotaDeCredito");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.NotaDeCredito objNotaDeCredito = objServWeb.ObtenerNotaDeCredito(pNumeroNotaDeCredito, pLoginWeb);
                if (objNotaDeCredito != null)
                {
                    resultado = dllFuncionesGenerales.ConvertToNotaDeCredito(objNotaDeCredito);
                    resultado.lista = ObtenerDetalleNotaDeCredito(pNumeroNotaDeCredito);
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroNotaDeCredito, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    public List<cNotaDeCreditoDetalle> ObtenerDetalleNotaDeCredito(string pNumeroNotaDeCredito)
    {
        try
        {
            List<cNotaDeCreditoDetalle> resultado = null;
            dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
            dkInterfaceWeb.NotaDeCreditoItemCOL objListaDetalle = objServWeb.ObtenerItemsDeNotaDeCredito(pNumeroNotaDeCredito);
            if (objListaDetalle != null)
            {
                resultado = new List<cNotaDeCreditoDetalle>();
                for (int i = 1; i <= objListaDetalle.Count(); i++)
                {
                    cNotaDeCreditoDetalle _objDetalleNotaDeCredito = new cNotaDeCreditoDetalle();
                    _objDetalleNotaDeCredito.Cantidad = objListaDetalle.get_Item(i).Cantidad == null ? "" : objListaDetalle.get_Item(i).Cantidad.ToString();
                    _objDetalleNotaDeCredito.Descripcion = objListaDetalle.get_Item(i).Descripcion;
                    _objDetalleNotaDeCredito.Importe = objListaDetalle.get_Item(i).Importe == null ? "" : objListaDetalle.get_Item(i).Importe.ToString();
                    _objDetalleNotaDeCredito.NumeroHoja = objListaDetalle.get_Item(i).NumeroHoja;
                    _objDetalleNotaDeCredito.NumeroItem = objListaDetalle.get_Item(i).NumeroItem;
                    _objDetalleNotaDeCredito.NumeroNotaDeCredito = objListaDetalle.get_Item(i).NumeroNotaDeCredito;
                    _objDetalleNotaDeCredito.PrecioPublico = objListaDetalle.get_Item(i).PrecioPublico == null ? "" : objListaDetalle.get_Item(i).PrecioPublico.ToString();
                    _objDetalleNotaDeCredito.PrecioUnitario = objListaDetalle.get_Item(i).PrecioUnitario == null ? "" : objListaDetalle.get_Item(i).PrecioUnitario.ToString();
                    _objDetalleNotaDeCredito.Troquel = objListaDetalle.get_Item(i).Troquel == null ? "" : objListaDetalle.get_Item(i).Troquel.ToString();
                    resultado.Add(_objDetalleNotaDeCredito);
                }
            }
            return resultado;
        }
        catch (Exception ex)
        {
            dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroNotaDeCredito);
            return null;
        }
    }
    //'B000101013330' HARLEY
    //[WebMethod]
    //public void ObtenerFactura_Prueba()
    //{
    //    object varW = ObtenerFactura("A001308376676", "romanello");
    //    object varR = ObtenerFactura("A001303461593", "RUBENE");
    //}
    [WebMethod]
    public cFactura ObtenerFactura(string pNumeroFactura, string pLoginWeb)
    {
        cFactura resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerFactura");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.Factura objFactura = objServWeb.ObtenerFactura(pNumeroFactura, pLoginWeb);
                if (objFactura != null)
                {
                    resultado = dllFuncionesGenerales.ConvertToFactura(objFactura);
                    resultado.lista = ObtenerDetalleFactura(pNumeroFactura);
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroFactura, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    public List<cFacturaDetalle> ObtenerDetalleFactura(string pNumeroFactura)
    {
        List<cFacturaDetalle> resultado = null;
        try
        {
            dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
            dkInterfaceWeb.FacturaItemCOL objListaDetalle = objServWeb.ObtenerItemsDeFactura(pNumeroFactura);
            if (objListaDetalle != null)
            {
                resultado = new List<cFacturaDetalle>();
                for (int i = 1; i <= objListaDetalle.Count(); i++)
                {
                    cFacturaDetalle _objDetalleFactura = new cFacturaDetalle();
                    _objDetalleFactura.Cantidad = objListaDetalle[i].Cantidad == null ? "" : objListaDetalle[i].Cantidad.ToString();
                    _objDetalleFactura.Caracteristica = objListaDetalle[i].Caracteristica == null ? "" : objListaDetalle[i].Caracteristica.ToString();
                    _objDetalleFactura.Descripcion = objListaDetalle[i].Descripcion;
                    _objDetalleFactura.Importe = objListaDetalle[i].Importe == null ? "" : objListaDetalle[i].Importe.ToString();
                    _objDetalleFactura.NumeroFactura = objListaDetalle[i].NumeroFactura;
                    _objDetalleFactura.NumeroHoja = objListaDetalle[i].NumeroHoja;
                    _objDetalleFactura.NumeroItem = objListaDetalle[i].NumeroItem;
                    _objDetalleFactura.PrecioPublico = objListaDetalle[i].PrecioPublico == null ? "" : objListaDetalle[i].PrecioPublico.ToString();
                    _objDetalleFactura.PrecioUnitario = objListaDetalle[i].PrecioUnitario == null ? "" : objListaDetalle[i].PrecioUnitario.ToString();
                    _objDetalleFactura.Troquel = objListaDetalle[i].Troquel == null ? "" : objListaDetalle[i].Troquel.ToString();
                    resultado.Add(_objDetalleFactura);
                }
            }
        }
        catch (Exception ex)
        {
            dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroFactura);
        }
        return resultado;
    }
    [WebMethod]
    public cDllRespuestaResumenAbierto ObtenerResumenAbierto(string pLoginWeb)
    {
        cDllRespuestaResumenAbierto resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerResumenAbierto");
            try
            {
                resultado = new cDllRespuestaResumenAbierto();
                resultado.lista = new List<cDllCtaResumenMovimiento>();
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado.isPoseeCuenta = objServWeb.PoseeCuentaResumen(pLoginWeb);
                resultado.ImporteTotal = objServWeb.ObtenerSaldoResumenAbierto(pLoginWeb);
                dkInterfaceWeb.CtaResumenMovimientoCOL movCuentaResumen = objServWeb.ObtenerMovimientosDeCuentaResumen(pLoginWeb);
                for (int i = 1; i <= movCuentaResumen.Count(); i++)
                {
                    cDllCtaResumenMovimiento objDllCtaResumenMovimiento = new cDllCtaResumenMovimiento();
                    objDllCtaResumenMovimiento.Fecha = movCuentaResumen[i].Fecha;
                    objDllCtaResumenMovimiento.FechaToString = objDllCtaResumenMovimiento.Fecha.ToShortDateString();
                    objDllCtaResumenMovimiento.Importe = movCuentaResumen[i].Importe;
                    objDllCtaResumenMovimiento.NumeroComprobante = movCuentaResumen[i].NumeroComprobante;
                    objDllCtaResumenMovimiento.TipoComprobante = dllFuncionesGenerales.ToConvert(movCuentaResumen[i].Comprobante);
                    objDllCtaResumenMovimiento.TipoComprobanteToString = dllFuncionesGenerales.ToConvertToString(movCuentaResumen[i].Comprobante);
                    resultado.lista.Add(objDllCtaResumenMovimiento);
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    //[WebMethod]
    //public void ObtenerMovimientosDeFichaCtaCte_Prueba()
    //{
    //    var dddd = ObtenerMovimientosDeFichaCtaCte("KAREHNKE", DateTime.Now.AddDays(-7), DateTime.Now.AddDays(1));
    //}
    [WebMethod]
    public List<cFichaCtaCte> ObtenerMovimientosDeFichaCtaCte(string pLoginWeb, DateTime pFechaDesde, DateTime pFechaHasta)
    {
        List<cFichaCtaCte> resultado = new List<cFichaCtaCte>();
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerMovimientosDeFichaCtaCte");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.MovimientoFichaCtaCteCOL movFichas = objServWeb.ObtenerMovimientosDeFichaCtaCte(pLoginWeb, pFechaDesde, pFechaHasta);
                for (int i = 1; i <= movFichas.Count(); i++)
                {
                    cFichaCtaCte obj = new cFichaCtaCte();
                    if (i == 1)
                    {
                        obj.Saldo = movFichas[i].Monto;
                    }
                    else
                    {
                        if (movFichas[i].Monto <= 0)
                        {
                            obj.Haber = movFichas[i].Monto;
                            obj.Saldo = obj.Haber + resultado[i - 2].Saldo;
                        }
                        else
                        {
                            obj.Debe = movFichas[i].Monto;
                            obj.Saldo = obj.Debe + resultado[i - 2].Saldo;
                        }
                    }
                    obj.Fecha = movFichas[i].FechaMovimiento;
                    obj.FechaToString = movFichas[i].FechaMovimiento.ToShortDateString();
                    obj.FechaVencimiento = movFichas[i].FechaVencimiento == null ? (DateTime?)null : (DateTime)movFichas[i].FechaVencimiento;
                    obj.FechaVencimientoToString = movFichas[i].FechaVencimiento == null ? string.Empty : ((DateTime)movFichas[i].FechaVencimiento).ToShortDateString();
                    obj.Comprobante = movFichas[i].NumeroComprobante;
                    obj.TipoComprobante = dllFuncionesGenerales.ToConvert(movFichas[i].TipoComprobante);
                    obj.TipoComprobanteToString = dllFuncionesGenerales.ToConvertToString(movFichas[i].TipoComprobante);
                    obj.Motivo = movFichas[i].Motivo == null ? string.Empty : movFichas[i].Motivo.ToString();
                    resultado.Add(obj);
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pFechaDesde, pFechaHasta);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    //[WebMethod]
    //public string ObtenerComprobantesEntreFechas11()
    //{
    //    string resultado = "Ok";
    //    ObtenerComprobantesEntreFechas("FAC", DateTime.Now.AddDays(-28), DateTime.Now.AddDays(-1), "ANA MARIA");
    //    return resultado;
    //}
    [WebMethod]
    public List<cComprobanteDiscriminado> ObtenerComprobantesEntreFechas(string pTipoComprobante, DateTime pDesde, DateTime pHasta, string pLoginWeb)
    {
        List<cComprobanteDiscriminado> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerComprobantesEntreFechas");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.ComprobDiscriminadoCOL objComprobantes = objServWeb.ObtenerComprobantesDePuntoDeVentaEntreFechas(pLoginWeb, pDesde, pHasta, pTipoComprobante);
                if (objComprobantes != null)
                {
                    resultado = new List<cComprobanteDiscriminado>();
                    if (objComprobantes.Count() > 0)
                    {

                        for (int i = 1; i <= objComprobantes.Count(); i++)
                        {
                            cComprobanteDiscriminado obj = new cComprobanteDiscriminado();
                            dkInterfaceWeb.ComprobanteDiscriminado objItem = objComprobantes.get_Item(i);
                            dkInterfaceWeb.ComprobanteDiscriminado r = new dkInterfaceWeb.ComprobanteDiscriminado();
                            obj.Comprobante = objItem.Comprobante;
                            obj.Destinatario = objItem.Destinatario;
                            obj.DetallePercepciones = objItem.DetallePercepciones != null ? objItem.DetallePercepciones.ToString() : "";
                            obj.Fecha = objItem.Fecha;
                            obj.FechaToString = objItem.Fecha != null ? objItem.Fecha.ToShortDateString() : "";
                            obj.MontoExento = objItem.MontoExento;
                            obj.MontoGravado = objItem.MontoGravado;
                            obj.MontoIvaInscripto = objItem.MontoIvaInscripto;
                            obj.MontoIvaNoInscripto = objItem.MontoIvaNoInscripto;
                            obj.MontoPercepcionDGR = objItem.MontoPercepcionesDGR;
                            obj.MontoTotal = objItem.MontoTotal;
                            obj.NumeroComprobante = objItem.NumeroComprobante;
                            resultado.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pTipoComprobante, pDesde, pHasta, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    [WebMethod]
    public List<cCtaCteMovimiento> ObtenerMovimientosDeCuentaCorriente(bool pIncluyeCancelados, DateTime pDesde, DateTime pHasta, string pLoginWeb)
    {
        List<cCtaCteMovimiento> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerMovimientosDeCuentaCorriente");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.CtaCteMovimientoCOL objCtaCteMovimiento = objServWeb.ObtenerMovimientosDeCuentaCorriente(pLoginWeb, pDesde, pHasta, pIncluyeCancelados);
                if (objCtaCteMovimiento != null)
                {
                    resultado = new List<cCtaCteMovimiento>();
                    if (objCtaCteMovimiento.Count() > 0)
                    {
                        DateTime dateValue;
                        for (int i = 1; i <= objCtaCteMovimiento.Count(); i++)
                        {
                            cCtaCteMovimiento obj = new cCtaCteMovimiento();
                            dkInterfaceWeb.CtaCteMovimiento objItem = objCtaCteMovimiento[i];
                            obj.Atraso = objItem.Atraso != null ? objItem.Atraso.ToString() : "";
                            obj.Fecha = DateTime.TryParse(objItem.Fecha.ToString(), out dateValue) ? (DateTime)objItem.Fecha : (DateTime?)null;
                            obj.FechaToString = obj.Fecha != null ? ((DateTime)obj.Fecha).ToShortDateString() : "";
                            obj.FechaPago = DateTime.TryParse(objItem.FechaPago.ToString(), out dateValue) ? (DateTime)objItem.FechaPago : (DateTime?)null;
                            obj.FechaPagoToString = obj.FechaPago != null ? ((DateTime)obj.FechaPago).ToShortDateString() : "";
                            obj.FechaVencimiento = DateTime.TryParse(objItem.FechaVencimiento.ToString(), out dateValue) ? (DateTime)objItem.FechaVencimiento : (DateTime?)null;
                            obj.FechaVencimientoToString = obj.FechaVencimiento != null ? ((DateTime)obj.FechaVencimiento).ToShortDateString() : "";
                            obj.Importe = objItem.Importe;
                            obj.MedioPago = objItem.MedioPago != null ? objItem.MedioPago.ToString() : "";
                            obj.NumeroComprobante = objItem.NumeroComprobante != null ? objItem.NumeroComprobante.ToString() : "";
                            obj.NumeroRecibo = objItem.NumeroRecibo != null ? objItem.NumeroRecibo.ToString() : "";
                            obj.Pago = objItem.Pago != null ? objItem.Pago.ToString() : ""; ;
                            obj.Saldo = objItem.Saldo;
                            obj.Semana = objItem.Semana != null ? objItem.Semana.ToString() : "";
                            obj.TipoComprobante = dllFuncionesGenerales.ToConvert(objItem.TipoComprobante);
                            obj.TipoComprobanteToString = dllFuncionesGenerales.ToConvertToString(objItem.TipoComprobante);
                            resultado.Add(obj);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIncluyeCancelados, pDesde, pHasta, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    //[WebMethod]
    //public string ObtenerMovimientosDeCuentaCorriente_Prueba()
    //{
    //    string resultado = "Ok";
    //    var lista = ObtenerMovimientosDeCuentaCorriente(false, new DateTime(2012, 11, 6), new DateTime(2013, 3, 20), "romanello");
    //    return resultado;
    //}
    //[WebMethod]
    //public string ObtenerComprobantesEntreFechas_Prueba()
    //{
    //    string resultado = "Ok";
    //    var lista = ObtenerComprobantesEntreFechas("FAC", DateTime.Now.AddDays(-14), DateTime.Now.AddDays(1), "romanello");
    //    return resultado;
    //}
    [WebMethod]
    public List<cDllPedido> ObtenerPedidosEntreFechas(DateTime pDesde, DateTime pHasta, string pLoginWeb)
    {
        List<cDllPedido> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerPedidosEntreFechas");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.PedidoCOL objPedidos = objServWeb.ObtenerPedidosDePuntoDeVentaEntreFechas(pLoginWeb, pDesde, pHasta);
                if (objPedidos != null)
                {
                    resultado = new List<cDllPedido>();
                    if (objPedidos.Count() > 0)
                    {
                        for (int i = 1; i <= objPedidos.Count(); i++)
                        {
                            //objPedidos[i].
                            cDllPedido obj = dllFuncionesGenerales.ToConvert(objPedidos[i]);
                            resultado.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pDesde, pHasta, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    //[WebMethod]
    //public string ObtenerPedidosEntreFechas_Prueba()
    //{
    //    string resultado = "Ok";
    //    var lista = ObtenerPedidosEntreFechas(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), "romanello");
    //    return resultado;
    //}
    [WebMethod]
    public cResumen ObtenerResumenCerrado(string pNumeroResumen, string pLoginWeb)
    {
        cResumen resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerResumenCerrado");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.Resumen objResumen = objServWeb.ObtenerResumenCerrado(pNumeroResumen);
                if (objResumen != null)
                {
                    resultado = dllFuncionesGenerales.ToConvert(objResumen);
                    dkInterfaceWeb.ResumenItemCOL objItemsResumen = objServWeb.ObtenerItemsDeResumenCerrado(pNumeroResumen);
                    if (objItemsResumen != null)
                    {
                        resultado.lista = new List<cResumenDetalle>();
                        for (int i = 1; i <= objItemsResumen.Count(); i++)
                        {
                            cResumenDetalle objResumenDetalle = dllFuncionesGenerales.ToConvert(objItemsResumen[i]);
                            resultado.lista.Add(objResumenDetalle);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroResumen, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    //[WebMethod]
    //public string ObtenerResumenCerrado_Prueba()
    //{
    //    string resultado = "Ok";

    //    var lista = ObtenerResumenCerrado("X000101900124", "JORGELINA");

    //    return resultado;
    //}
    [WebMethod]
    public decimal? ObtenerSaldoCtaCteAFecha(string pLoginWeb, DateTime pFecha)
    {
        decimal? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerSaldoCtaCteAFecha");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.ObtenerSaldoCtaCteAFecha(pLoginWeb, pFecha);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pFecha);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    [WebMethod]
    public List<cDllChequeRecibido> ObtenerChequesEnCartera(string pLoginWeb)
    {
        List<cDllChequeRecibido> resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerChequesEnCartera");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.ChequeRecibidoCOL objChequesRecibidos = objServWeb.ObtenerChequesEnCartera(pLoginWeb);
                if (objChequesRecibidos != null)
                {
                    resultado = new List<cDllChequeRecibido>();
                    for (int i = 1; i <= objChequesRecibidos.Count(); i++)
                    {
                        cDllChequeRecibido objCheque = dllFuncionesGenerales.ConvertToChequeRecibido(objChequesRecibidos[i]);// new cDllChequeRecibido();
                        if (objCheque != null)
                        {
                            resultado.Add(objCheque);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    [WebMethod]
    public decimal? ObtenerSaldoChequesEnCartera(string pLoginWeb)
    {
        decimal? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerSaldoChequesEnCartera");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.ObtenerSaldoChequesEnCartera(pLoginWeb);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    [WebMethod]
    public decimal? ObtenerSaldoResumenAbierto(string pLoginWeb)
    {
        decimal? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerSaldoResumenAbierto");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.ObtenerSaldoResumenAbierto(pLoginWeb);
            }

            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    //[WebMethod]
    //public void ObtenerSaldosPresentacionParaComposicion_Prueba()
    //{
    //    string pLoginWeb = "aquiles08";
    //    DateTime pFecha = new DateTime(2018, 05, 08);
    //    var dfff = cBaseDatos.RecuperarClienteAdministradorPorIdUsuarios(35);
    //    cDllSaldosComposicion fff = ObtenerSaldosPresentacionParaComposicion(pLoginWeb, pFecha);
    //}
    [WebMethod]
    public cDllSaldosComposicion ObtenerSaldosPresentacionParaComposicion(string pLoginWeb, DateTime pFecha)
    {
        cDllSaldosComposicion resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerSaldosPresentacionParaComposicion");
            try
            {
                resultado = new cDllSaldosComposicion();
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                try
                {
                    resultado.SaldoResumenAbierto = objServWeb.ObtenerSaldoResumenAbierto(pLoginWeb);
                }
                catch (Exception ex1)
                {
                    dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex1, DateTime.Now, pLoginWeb, pFecha);
                }

                try
                {
                    resultado.SaldoChequeCartera = objServWeb.ObtenerSaldoChequesEnCartera(pLoginWeb);
                }
                catch (Exception ex2) { dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex2, DateTime.Now, pLoginWeb, pFecha); }
                try
                {
                    resultado.SaldoCtaCte = objServWeb.ObtenerSaldoCtaCteAFecha(pLoginWeb, pFecha);
                }
                catch (Exception ex3) { dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex3, DateTime.Now, pLoginWeb, pFecha); }
                try
                {
                    resultado.SaldoTotal = objServWeb.ObtenerSaldoTotal(pLoginWeb);
                }
                catch (Exception ex4) { dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex4, DateTime.Now, pLoginWeb, pFecha); }
                resultado.isPoseeCuentaResumen = objServWeb.PoseeCuentaResumen(pLoginWeb);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pFecha);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    //[WebMethod]
    //public void ObtenerSaldosPresentacionParaComposicion_Prueba()
    //{
    //    DateTime fecha = new DateTime(2013, 4, 20);
    //    var fdf = ObtenerSaldosPresentacionParaComposicion("ROSARIO", fecha);
    //}
    //[WebMethod]
    //public string ObtenerTipoDeComprobanteAMostrar_Prueba()
    //{

    //    var fd = ObtenerTipoDeComprobanteAMostrar("ACETONAVARRO");
    //    return fd.ToString();
    //}
    [WebMethod]
    public string ObtenerTipoDeComprobanteAMostrar(string pLoginWeb)
    {
        string resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerTipoDeComprobanteAMostrar");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.ObtenerTipoDeComprobanteAMostrar(pLoginWeb);
            }

            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    //[WebMethod]
    //public string ObtenerTipoDeComprobanteAMostrar_Prueba()
    //{
    //    string resultado = "Ok";
    //    var lista = ObtenerTipoDeComprobanteAMostrar("romanello");
    //    return resultado;
    //}
    //[WebMethod]
    //public List<cDllPedidoTransfer> TomarPedidoDeTransfers(string pLoginCliente, int pIdTransfer, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto)
    //{
    //    List<cDllPedidoTransfer> lista = null;
    //    //if (VerificarPermisos(CredencialAutenticacion))
    //    {
    //        lista = new List<cDllPedidoTransfer>();
    //        try
    //        {
    //            dkInterfaceWeb.PedidoCOL Resultado;
    //            dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
    //            dkInterfaceWeb.PedidoTransfer pedidoTransfer = new dkInterfaceWeb.PedidoTransfer();
    //            foreach (cDllProductosAndCantidad item in pListaProducto)
    //            {
    //                pedidoTransfer.Add(item.codProductoNombre, item.cantidad, pIdTransfer);
    //            }
    //            pedidoTransfer.Login = pLoginCliente;
    //            pedidoTransfer.MensajeEnFactura = pMensajeEnFactura;
    //            pedidoTransfer.MensajeEnRemito = pMensajeEnRemito;

    //            dkInterfaceWeb.TipoEnvio tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
    //            switch (pTipoEnvio)
    //            {
    //                case "E":
    //                    tipoEnvio = dkInterfaceWeb.TipoEnvio.Encomienda;
    //                    break;
    //                case "R":
    //                    tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
    //                    break;
    //                case "C":
    //                    tipoEnvio = dkInterfaceWeb.TipoEnvio.Cadeteria;
    //                    break;
    //                case "M":
    //                    tipoEnvio = dkInterfaceWeb.TipoEnvio.Mostrador;
    //                    break;
    //            }
    //            Resultado = objServWeb.TomarPedidoDeTransfers(pedidoTransfer, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL");
    //            for (int i = 1; i <= Resultado.Count(); i++)
    //            {
    //                lista.Add(dllFuncionesGenerales.ToConvertTransfer(Resultado[i]));
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            dllFuncionesGenerales.grabarLog(@"TomarPedidoDeTransfers", ex, DateTime.Now);
    //            return null;
    //        }
    //    }
    //    return lista;
    //}
    [WebMethod]
    public List<cDllPedidoTransfer> TomarPedidoDeTransfers(string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto)
    {
        List<cDllPedidoTransfer> lista = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("TomarPedidoDeTransfers");
            try
            {
                lista = new List<cDllPedidoTransfer>();
                dkInterfaceWeb.PedidoCOL Resultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.PedidoTransfer pedidoTransfer = new dkInterfaceWeb.PedidoTransfer();
                foreach (cDllProductosAndCantidad item in pListaProducto)
                {
                    pedidoTransfer.Add(item.codProductoNombre, item.cantidad, item.IdTransfer);
                }
                pedidoTransfer.Login = pLoginCliente;
                pedidoTransfer.MensajeEnFactura = pMensajeEnFactura;
                pedidoTransfer.MensajeEnRemito = pMensajeEnRemito;

                dkInterfaceWeb.TipoEnvio tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
                switch (pTipoEnvio)
                {
                    case "E":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Encomienda;
                        break;
                    case "R":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
                        break;
                    case "C":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Cadeteria;
                        break;
                    case "M":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Mostrador;
                        break;
                }
                Resultado = objServWeb.TomarPedidoDeTransfers(pedidoTransfer, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL");
                if (Resultado != null)
                {
                    for (int i = 1; i <= Resultado.Count(); i++)
                    {
                        lista.Add(dllFuncionesGenerales.ToConvertTransfer(Resultado[i]));
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return lista;
    }
    [WebMethod]
    public void ImprimirComprobante(string pTipoComprobante, string pNroComprobante)
    {
        //if (VerificarPermisos(CredencialAutenticacion))
        {
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objServWeb.ImprimirComprobante(pTipoComprobante, pNroComprobante);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pTipoComprobante, pNroComprobante);
                // return null;
            }
        }
    }
    //[WebMethod]
    //public void ImprimirComprobPDFWEB_Prueba()
    //{
    //    cFactura o = ObtenerFactura("A001304387440", "romanello");
    //    ImprimirComprobante("TRZ", o.NumeroRemito);
    //    //ImprimirComprobPDFWEB("TRZ", o.NumeroRemito);
    //    //ImprimirComprobante("FAC", o.NumeroRemito);
    //    //ImprimirComprobPDFWEB("FAC", o.NumeroRemito);
    //}
    [WebMethod]
    public void ImprimirComprobPDFWEB(string pTipoComprobante, string pNroComprobante)
    {
        //if (VerificarPermisos(CredencialAutenticacion))
        {
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objServWeb.ImprimirComprobPDFWEB(pTipoComprobante, pNroComprobante);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pTipoComprobante, pNroComprobante);
                // return null;
            }
        }
    }

    [WebMethod]
    public List<cPlan> ObtenerPlanesDeObrasSociales()
    {
        List<cPlan> lista = null;
        //if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerPlanesDeObrasSociales");
            try
            {
                lista = new List<cPlan>();
                dkInterfaceWeb.PlanCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerPlanesDeObrasSociales();
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado[i])));
                else
                    return null;
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return lista;
    }
    // [WebMethod]
    // public void ObtenerPlanillasObraSocialClientesDeObraSocialyPeriodo_Prueba()
    // {
    //    var ll = ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes("PAMI NC", "romanello", 2015, 5);
    //     dkInterfaceWeb.PlanillaObSocCOL objResultado;
    //     dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
    //     //objResultado = objServWeb.ObtenerPlanillasObraSocialClientesDeObraSocialyPeriodo_Prueba() ;
    //     objResultado = objServWeb.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes("PAMI NC", "romanello", 2015, 5);
    //// string NombrePlan, string LoginWeb, int Anio, string Mes, string Quincena, string Semana
    //     //var lll = ObtenerPlanillasObraSocialClientesDeObraSocialyPeriodo("PAMI NC", "romanello", 2015, "6", null, null);
    //     var lll = ObtenerPlanillasObraSocialClientesDeObraSocialyPeriodo("PAMI NC", "romanello", 2015, 5, null, null);
    //     var lll2 = ObtenerPlanillasObraSocialClientesDeObraSocialyPeriodo("PRESERFAR", "romanello", 2015, null, null, 20);
    // }
    [WebMethod]
    public List<cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana(string NombrePlan, string LoginWeb, int Anio, int Semana)
    {

        List<cPlanillaObSoc> lista = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana");
            try
            {
                lista = new List<cPlanillaObSoc>();
                dkInterfaceWeb.PlanillaObSocCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana(NombrePlan, LoginWeb, Anio, Semana);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado.get_Item(i))));
                else
                    return null;
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NombrePlan, LoginWeb, Anio, Semana);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return lista;
    }
    [WebMethod]
    public List<cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMesQuincena(string NombrePlan, string LoginWeb, int Anio, int Mes, int Quincena)
    {

        List<cPlanillaObSoc> lista = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMesQuincena");
            try
            {
                lista = new List<cPlanillaObSoc>();
                dkInterfaceWeb.PlanillaObSocCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMesQuincena(NombrePlan, LoginWeb, Anio, Mes, Quincena);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado.get_Item(i))));
                else
                    return null;
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NombrePlan, LoginWeb, Anio, Mes, Quincena);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return lista;
    }
    [WebMethod]
    public List<cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes(string NombrePlan, string LoginWeb, int Anio, int Mes)
    {

        List<cPlanillaObSoc> lista = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes");
            try
            {
                lista = new List<cPlanillaObSoc>();
                dkInterfaceWeb.PlanillaObSocCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes(NombrePlan, LoginWeb, Anio, Mes);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado.get_Item(i))));
                else
                    return null;
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NombrePlan, LoginWeb, Anio, Mes);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return lista;
    }
    [WebMethod]
    public bool? FacturaTrazable(string pNumeroFactura)
    {
        bool? resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("FacturaTrazable");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.FacturaTrazable(pNumeroFactura);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroFactura);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    //[WebMethod]
    //public cObraSocialCliente ObtenerObraSocialCliente_Prueba()
    //{
    //    var lla = ObtenerObraSocialCliente("X000100003305", "esteban");
    //   var llllo = ObtenerItemsDeObraSocialCliente("X0001-00003305");
    //   var llllo1 = ObtenerItemsDeObraSocialCliente("00003305");
    //   return lla;
    //}
    [WebMethod]
    public cObraSocialCliente ObtenerObraSocialCliente(string pNumeroObraSocialCliente, string pLoginWeb)
    {
        cObraSocialCliente resultado = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("ObtenerObraSocialCliente");
            try
            {
                dkInterfaceWeb.ObraSocialCliente objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerObraSocialCliente(pNumeroObraSocialCliente, pLoginWeb);
                resultado = dllFuncionesGenerales.ToConvert(objResultado);
                resultado.lista = ObtenerItemsDeObraSocialCliente(pNumeroObraSocialCliente);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroObraSocialCliente, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return resultado;
    }
    //[WebMethod]
    public List<cObraSocialClienteItem> ObtenerItemsDeObraSocialCliente(string pNumeroObraSocialCliente)
    {
        List<cObraSocialClienteItem> lista = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            lista = new List<cObraSocialClienteItem>();
            try
            {
                dkInterfaceWeb.ObraSocialClienteItemCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerItemsDeObraSocialCliente(pNumeroObraSocialCliente);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado.get_Item(i))));
                else
                    return null;
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroObraSocialCliente);
                return null;
            }
            finally
            {
            }
        }
        return lista;
    }
    //[WebMethod]
    //public void ObtenerUltimos10ResumenesDePuntoDeVenta_Prueba()
    //{
    //    var obj = ObtenerUltimos10ResumenesDePuntoDeVenta("romanello");
    //    var obj1 = ObtenerComprobantesAImprimirEnBaseAResumen(obj.Last().Numero);
    //    int rr = 4;
    //}
    [WebMethod]
    public List<cResumen> ObtenerUltimos10ResumenesDePuntoDeVenta(string pLoginWeb)
    {
        List<cResumen> lista = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            lista = new List<cResumen>();
            try
            {
                dkInterfaceWeb.ResumenCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerUltimos10ResumenesDePuntoDeVenta(pLoginWeb);
                if (objResultado != null)
                {
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado[i])));
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            finally
            {
            }
        }
        return lista;
    }
    [WebMethod]
    public List<cCbteParaImprimir> ObtenerComprobantesAImprimirEnBaseAResumen(string pNumeroResumen)
    {
        List<cCbteParaImprimir> lista = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            lista = new List<cCbteParaImprimir>();
            try
            {
                dkInterfaceWeb.CbteParaImprimirCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerComprobantesAImprimirEnBaseAResumen(pNumeroResumen);
                if (objResultado != null)
                {
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado[i])));
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroResumen);
                return null;
            }
            finally
            {
            }
        }
        return lista;
    }
    [WebMethod]
    public decimal? ObtenerCreditoDisponibleSemanal(string pLoginWeb)
    {
        decimal? result = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                var objResultado = objServWeb.ObtenerCreditoDisponibleSemanal(pLoginWeb);
                if (objResultado != null)
                {
                    result = Convert.ToDecimal(objResultado);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            finally
            {
            }
        }
        return result;
    }
    [WebMethod]
    public decimal? ObtenerCreditoDisponibleTotal(string pLoginWeb)
    {
        decimal? result = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                result = objServWeb.ObtenerCreditoDisponibleTotal(pLoginWeb);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            finally
            {
            }
        }
        return result;
    }
    [WebMethod]
    public List<cConsObraSocial> ObtenerComprobantesObrasSocialesDePuntoDeVentaEntreFechas(string pLoginWeb, string pPlan, DateTime pFechaDesde, DateTime pFechaHasta)
    {
        List<cConsObraSocial> lista = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            lista = new List<cConsObraSocial>();
            try
            {
                dkInterfaceWeb.ConsObraSocialCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerComprobantesObrasSocialesDePuntoDeVentaEntreFechas(pLoginWeb, pPlan, pFechaDesde, pFechaHasta);

                if (objResultado != null)
                {
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado[i])));
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pPlan, pFechaDesde, pFechaHasta);
                return null;
            }
            finally
            {
            }
        }
        return lista;
    }
    // 2019/04/11
    [WebMethod]
    public cDllPedido TomarPedidoConIdCarrito(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto, bool pIsUrgente)
    {
        cDllPedido ResultadoFinal = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("TomarPedidoConIdCarrito");
            //ResultadoFinal = new cDllPedido();
            try
            {
                dkInterfaceWeb.Pedido Resultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.Pedido pedido = new dkInterfaceWeb.Pedido();

                // llenar datos pedidos
                pedido.Login = pLoginCliente;
                pedido.MensajeEnFactura = pMensajeEnFactura;
                pedido.MensajeEnRemito = pMensajeEnRemito;

                //List<cDllProductosAndCantidad> ListaProducto = Serializador.DeserializarJson<List<cDllProductosAndCantidad>>(pListaProducto);
                // Cargar productos al carrito
                foreach (cDllProductosAndCantidad item in pListaProducto)
                {
                    pedido.Add(item.codProductoNombre, item.cantidad, item.isOferta ? "$" : " ");
                }
                dkInterfaceWeb.TipoEnvio tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
                switch (pTipoEnvio)
                {
                    case "E":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Encomienda;
                        break;
                    case "R":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
                        break;
                    case "C":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Cadeteria;
                        break;
                    case "M":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Mostrador;
                        break;
                    //default:
                    //    break;
                }

                Resultado = objServWeb.TomarPedidoConIdCarrito(pIdCarrito, pedido, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL", pIsUrgente);
                if (Resultado != null)
                {
                    ResultadoFinal = dllFuncionesGenerales.ToConvert(Resultado);
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pIsUrgente);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return ResultadoFinal;
    }
    [WebMethod]
    public List<cDllPedidoTransfer> TomarPedidoDeTransfersConIdCarrito(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto)
    {
        List<cDllPedidoTransfer> lista = null;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            classTiempo tiempo = new classTiempo("TomarPedidoDeTransfersConIdCarrito");
            try
            {
                lista = new List<cDllPedidoTransfer>();
                dkInterfaceWeb.PedidoCOL Resultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.PedidoTransfer pedidoTransfer = new dkInterfaceWeb.PedidoTransfer();
                foreach (cDllProductosAndCantidad item in pListaProducto)
                {
                    pedidoTransfer.Add(item.codProductoNombre, item.cantidad, item.IdTransfer);
                }
                pedidoTransfer.Login = pLoginCliente;
                pedidoTransfer.MensajeEnFactura = pMensajeEnFactura;
                pedidoTransfer.MensajeEnRemito = pMensajeEnRemito;

                dkInterfaceWeb.TipoEnvio tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
                switch (pTipoEnvio)
                {
                    case "E":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Encomienda;
                        break;
                    case "R":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
                        break;
                    case "C":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Cadeteria;
                        break;
                    case "M":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Mostrador;
                        break;
                }
                Resultado = objServWeb.TomarPedidoDeTransfersConIdCarrito(pIdCarrito, pedidoTransfer, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL");
                if (Resultado != null)
                {
                    for (int i = 1; i <= Resultado.Count(); i++)
                    {
                        lista.Add(dllFuncionesGenerales.ToConvertTransfer(Resultado[i]));
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
        }
        return lista;
    }
    [WebMethod]
    public bool ValidarExistenciaDeCarritoWebPasado(int pIdCarrito)
    {
        if (VerificarPermisos(CredencialAutenticacion))
        {
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                return objServWeb.ValidarExistenciaDeCarritoWebPasado(pIdCarrito);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito);
                return false;
            }
        }
        return false;
    }
    [WebMethod]
    public double ObtenerSaldoFinalADiciembrePorCliente(string LoginWeb)
    {
        double resultado = 0;
        if (VerificarPermisos(CredencialAutenticacion))
        {
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.ObtenerSaldoFinalADiciembrePorCliente(LoginWeb);
            }
            catch (Exception ex)
            {
                dllFuncionesGenerales.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, LoginWeb);
            }
        }
        return resultado;
    }
}
