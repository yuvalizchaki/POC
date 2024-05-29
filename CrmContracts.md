# Configurations: Field Components

    0: TextFieldConfiguration,
    1: SwitchConfiguration,
    2: NumberFieldConfiguration,

# OrderStatus

    Draft = 1,
    Hidden = 2,
    Approved = 3,
    Completed = 4,
    Canceled = 5
    Returned = 6
    Ready = 7

- Logic flow of Order Statuses:

  [```Hidden```] - **`Draft`** - **`Approved`** - **`Ready`** &nbsp;&nbsp;|&nbsp;&nbsp; **_`Completed`_** - **_`Returned`_** - [```Canceled```]

# Providers

- ### Module Types

      Accounting = 1,

- ### Provider Types

      QuickBooks = 1,
      GreenInvoice = 2,

- ### Accounting Types (OwnAccounting)
      QuickBooksSandbox = 1,
      QuickBooksLive = 2,
      GreenInvoiceSandbox = 3,
      GreenInvoiceLive = 4,

# ModulePermissionType

    SystemUser = 0,
    Customer = 1,

# FileTargetCode

    Product = 1,
    ServiceProfile = 2,
    SystemUser = 3,
    Customer = 4,
    Company = 5,
    PeopleProfile = 6,
