using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class ViewModelCustomer : INotifyPropertyChanged
{
    private Customer _cust = new Customer();
    public Customer Cust
    {
        get { return _cust; }
        set { _cust = value; }
    }

    public string TextName
    {
        get { return _cust.name; }
        set { _cust.name = value; NotifyPropertyChanged(); }
    }

    public string TextAddress
    {
        get { return _cust.address; }
        set { _cust.address = value; NotifyPropertyChanged(); }
    }

    public int TextTelefonNr
    {
        get { return _cust.telefonNr; }
        set { _cust.telefonNr = value; NotifyPropertyChanged(); }
    }

    public string TextCustomerInfo
    {
        get { return _cust.customerInfo; }
        set { _cust.customerInfo = value; NotifyPropertyChanged(); }
    }

    public string TextContactPerson
    {
        get { return _cust.contactPerson; }
        set { _cust.contactPerson = value; NotifyPropertyChanged(); }
    }
    
    public int TextGrossIncome
    {
        get { return _cust.grossIncome; }
        set { _cust.grossIncome = value; NotifyPropertyChanged(); }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        //PropertyChanged?.Invoke(this. new P)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

}
