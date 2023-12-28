// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using ResponseFilter;
using ResponseFilterConsole;


var fields = new Fields();

fields.ResponseFields = "Address";

var data1 = new Data()
{
    FirstName = "test1",
    LastName = "hello",
    Phone = "09876532245",
    Email = "tes@tes.com"
};

Data test = null;


var check = Filter<Data>.GetFilteredResponse(data1, fields);


Console.WriteLine(check);



