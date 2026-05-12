using System.IO;
using ExcelDataReader;

// Required for .NET Core to support additional encodings
namespace App1{
public class Model
{
    public object _Название{get;set;}
    public object _Улица{get;set;}
    public object _Находится_в_ТЦ{get;set;}
    public object _Идентификатор{get;set;}
    public object _Идентификаторы_конкурентов_{get;set;}
    public object _Парковка{get;set;}
    public object _Вид_входа{get;set;}
    public object _Этаж{get;set;}
    public object _Средняя_оценка_по_данным_яндекс_карт{get;set;}
    public object _Количество_отзывов_до_01_01_2026_{get;set;}
    public object _Среднее_кол_во_отзывов_в_год_до_01_01_2026_{get;set;}
    public object _Учитывать_ли_строку_при_обработке_данных_{get;set;}

}
public class OutModel
{
    public string _Название{get;set;}
    public string _Улица{get;set;}
    public int _Находится_в_ТЦ{get;set;}
    
    public int _Количество_Конкурентов{get;set;}
    public string _Парковка{get;set;}
    public string _Вид_входа{get;set;}
    public int _ТипПарковки{get;set;}
    public int _ТипВхода{get;set;}
    public int _Этаж{get;set;}
    public Double _Средняя_оценка_по_данным_яндекс_карт{get;set;}
    public double _Количество_отзывов_до_01_01_2026_{get;set;}
    public double _Среднее_кол_во_отзывов_в_год_до_01_01_2026_{get;set;}
    public int _ВыходнаяОценка{get;set;}

}
public class Program
{
    static void Main(string[]args)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
string inpName="СистПринРешВходныеДанные.xlsx";
///https://github.com/ExcelDataReader/ExcelDataReader
string zeroRow="";
for(int i=0;i<12;i++)
zeroRow+="_";
var inpValues=new List<string[]>();
using (var stream = File.Open(inpName, FileMode.Open, FileAccess.Read,FileShare.Read))
{
    using (var reader = ExcelReaderFactory.CreateReader(stream))
    {
        //Кол-во столбцов
        int colCount=reader.FieldCount;
        Console.WriteLine(colCount);
        
       
        while(reader.Read())
        {
            string[]cols=new string[colCount];
          string tmp_=""; 
            //reader.Read(); 
          for(int cid=0;cid<colCount;cid++)
          {
            
            cols[cid]=Convert.ToString(reader.GetValue(cid));
            tmp_+=cols[cid]+"_";
          }
          Console.WriteLine(tmp_);
          if(tmp_==zeroRow){
            break;
          }
          inpValues.Add(cols);
        }
        //while(reader.NextResult());
    }
}
string code="\n class Model \n{";
foreach(var v in inpValues[0]){
    Console.Write(v.ToString() +"_");
    code+=$"public object _{v.Replace(" ","_")}"+"{get;set;} \n";
}
code+="\n }\n";
Console.WriteLine(code);
/*class Model{
   public string _Название;

}*/

var models=new Model[inpValues.Count-1];
for(int n=0;n<models.Length;n++)
{
    int apos=0;
    models[n]=new Model();
    models[n]._Название=Convert.ToString(inpValues[n+1][apos++]);
    models[n]._Улица=Convert.ToString(inpValues[n+1][apos++]);
    models[n]._Находится_в_ТЦ=Convert.ToBoolean(inpValues[n+1][apos++].ToLower()=="да");
    models[n]._Идентификатор=Convert.ToInt32(inpValues[n+1][apos++]);
    string concurs=inpValues[n+1][apos++];
    if(concurs!=""){
        models[n]._Идентификаторы_конкурентов_=concurs.Split("#");
    }
    else{
        models[n]._Идентификаторы_конкурентов_=new string[0];
    }
    models[n]._Парковка=Convert.ToString(inpValues[n+1][apos++]);
    models[n]._Вид_входа=Convert.ToString(inpValues[n+1][apos++]);
    models[n]._Этаж=Convert.ToInt32(inpValues[n+1][apos++]);
    string _сред_оцен=inpValues[n+1][apos++];
    if(Double.TryParse(_сред_оцен,out Double v)){
         models[n]._Средняя_оценка_по_данным_яндекс_карт=v;
    }else{
        models[n]._Средняя_оценка_по_данным_яндекс_карт=Convert.ToDouble(0);
    }
    string _колво_отзывов=inpValues[n+1][apos++];
    if(Double.TryParse(_колво_отзывов,out v)){
         models[n]._Количество_отзывов_до_01_01_2026_=v;
    }else{
        models[n]._Количество_отзывов_до_01_01_2026_=Convert.ToDouble(0);
    }

    string _среднееколвоотзывов=inpValues[n+1][apos++];
    if(Double.TryParse(_среднееколвоотзывов,out v)){
         models[n]._Среднее_кол_во_отзывов_в_год_до_01_01_2026_=v;
    }else{
        models[n]._Среднее_кол_во_отзывов_в_год_до_01_01_2026_=Convert.ToDouble(0);
    }
    models[n]._Учитывать_ли_строку_при_обработке_данных_=Convert.ToBoolean(inpValues[n+1][apos++].ToLower()!="нет");
}
HashSet<string>_типыпарковки=new HashSet<string>();
HashSet<string>_типвхода=new HashSet<string>();
foreach(var m in models){
    _типыпарковки.Add(m._Парковка as string);
    _типвхода.Add(m._Вид_входа as string);
}
var resm=new List<OutModel>();
foreach(var m in models){
    if(Convert.ToBoolean(m._Учитывать_ли_строку_при_обработке_данных_)==true){
        var omod=new OutModel();
        omod._Название=m._Название as string;
        if(omod._Название!=null){
            omod._Название=omod._Название.Replace(" ","_").Replace(",","_");
        }
        omod._Улица=m._Улица as string;
        if(omod._Улица!=null){
            omod._Улица=omod._Улица.Replace(" ","_").Replace(",","_");
        }
        omod._Находится_в_ТЦ=(Convert.ToBoolean(m._Находится_в_ТЦ )==true)?1:0;
        omod._Количество_Конкурентов=(m._Идентификаторы_конкурентов_ as string[]).Length;
        omod._Парковка=m._Парковка as string;
        if(omod._Парковка!=null){
            omod._Парковка=omod._Парковка.Replace(" ","_").Replace(",","_");
        }
        omod._ТипПарковки=Array.IndexOf(_типыпарковки.ToArray(),omod._Парковка);
        omod._Вид_входа=m._Вид_входа as string;
        if(omod._Вид_входа!=null){
            omod._Вид_входа=omod._Вид_входа.Replace(" ","_").Replace(",","_");
        }
        omod._ТипВхода=Array.IndexOf(_типвхода.ToArray(),omod._Вид_входа);
        if(omod._ТипВхода!=null){
            omod._ТипВхода=omod._ТипВхода.Replace(" ","_").Replace(",","_");
        }
        omod._Этаж =Convert.ToInt32( m._Этаж) ;
        omod._Среднее_кол_во_отзывов_в_год_до_01_01_2026_=Convert.ToDouble(m._Среднее_кол_во_отзывов_в_год_до_01_01_2026_);
        omod._Количество_отзывов_до_01_01_2026_=Convert.ToInt32(m._Количество_отзывов_до_01_01_2026_);
        omod._Средняя_оценка_по_данным_яндекс_карт=Convert.ToDouble(m._Средняя_оценка_по_данным_яндекс_карт);
        if(omod._Средняя_оценка_по_данным_яндекс_карт<0.51){
            omod._ВыходнаяОценка=0;
        }else if(omod._Средняя_оценка_по_данным_яндекс_карт<1.51){
            omod._ВыходнаяОценка=1;
        }else if(omod._Средняя_оценка_по_данным_яндекс_карт<2.51){
            omod._ВыходнаяОценка=2;
        }else if(omod._Средняя_оценка_по_данным_яндекс_карт<3.51){
            omod._ВыходнаяОценка=3;
        }else if(omod._Средняя_оценка_по_данным_яндекс_карт<4.51){
            omod._ВыходнаяОценка=4;
        }else{
            omod._ВыходнаяОценка=5;
        }
        //omod._Идентификатор=m._Идентификатор as int;
    }
}

}}
}