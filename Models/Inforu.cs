﻿
using System;
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]

public partial class Inforu
{

    private InforuRootInforuUser userField;

    private InforuRootInforuContent contentField;

    private InforuRootInforuRecipients recipientsField;

    private InforuRootInforuSettings settingsField;

    /// <remarks/>
    public InforuRootInforuUser User
    {
        get
        {
            return this.userField;
        }
        set
        {
            this.userField = value;
        }
    }

    /// <remarks/>
    public InforuRootInforuContent Content
    {
        get
        {
            return this.contentField;
        }
        set
        {
            this.contentField = value;
        }
    }

    ///// <remarks/>
    public InforuRootInforuRecipients Recipients
    {
        get
        {
            return this.recipientsField;
        }
        set
        {
            this.recipientsField = value;
        }
    }

    ///// <remarks/>
    public InforuRootInforuSettings Settings
    {
        get
        {
            return this.settingsField;
        }
        set
        {
            this.settingsField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InforuRootInforuUser
{

    private string usernameField;

    private string passwordField;

    /// <remarks/>
    public string Username
    {
        get
        {
            return this.usernameField;
        }
        set
        {
            this.usernameField = value;
        }
    }

    /// <remarks/>
    public string Password
    {
        get
        {
            return this.passwordField;
        }
        set
        {
            this.passwordField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InforuRootInforuContent
{

    private string messageField;

    private string typeField;

    /// <remarks/>
    public string Message
    {
        get
        {
            return this.messageField;
        }
        set
        {
            this.messageField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Type
    {
        get
        {
            return this.typeField;
        }
        set
        {
            this.typeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InforuRootInforuRecipients
{

    private string phoneNumberField;

    /// <remarks/>
    public string PhoneNumber
    {
        get
        {
            return this.phoneNumberField;
        }
        set
        {
            this.phoneNumberField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InforuRootInforuSettings
{

    private string senderNameField;

    private string senderNumberField;

    private string customerMessageIDField;


    private string deliveryNotificationUrlField;

    /// <remarks/>
    public string SenderName
    {
        get
        {
            return this.senderNameField;
        }
        set
        {
            this.senderNameField = value;
        }
    }

    /// <remarks/>
    public string SenderNumber
    {
        get
        {
            return this.senderNumberField;
        }
        set
        {
            this.senderNumberField = value;
        }
    }

    /// <remarks/>
    public string CustomerMessageID
    {
        get
        {
            return this.customerMessageIDField;
        }
        set
        {
            this.customerMessageIDField = value;
        }
    }

    /// <remarks/>
   
    public string DeliveryNotificationUrl
    {
        get
        {
            return this.deliveryNotificationUrlField;
        }
        set
        {
            this.deliveryNotificationUrlField = value;
        }
    }
}

