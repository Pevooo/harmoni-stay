import pyodbc
import random
import datetime
import bcrypt
import time

"""
CONSTANTS
"""

BUILDING_NO = 3
FLOOR_NO = 4
ROOM_NO = 15
EMPLOYEE_NO = 60
EVENTS_NO = 7
GUEST_MAX_COUNT = 200


def main():


    choice = input("1) Local Database\n2) Hosted Database\n")

    connection = None
    match choice:
        case '1':
            connection = pyodbc.connect(r"DRIVER={ODBC Driver 17 for SQL Server};server=localhost;Database=Harmonistay;Trusted_Connection=yes;TrustServerCertificate=yes")
        case '2':
            print("Please wait, this might take a while..")
            connection = pyodbc.connect(r"DRIVER={ODBC Driver 17 for SQL Server};server=sql.bsite.net\MSSQL2016;Database=harmonistay_SampleDB;UID=harmonistay_SampleDB;PWD=harmonistay")
            print("Connected\nFilling Data...")

    cursor = connection.cursor()
    
    
    try:
        delete_all(cursor)
    except:
        pass

    male_names = ["Ahmed", "Mohamed", "Kareem", "Amr", "Omar", "Kamel", "Mostafa", "Ehab", "Seif", "Hassan", "Abdullah", "Ali", "Hatem", "Mohsen", "Yaseen", "Rami", "Youssef"]
    female_names = ["Fatma", "Aya", "Alaa", "Nesma", "Ghada", "Sara", "Salma", "Mariam", "Sama", "Samira", "Farah", "Fatma"]
    all_names = male_names + female_names
    facilities = ["Main Restaurant", "Room Service", "Kids Club", "Aqua Park", "Animation", "Main Swimming Pool", "Kids Swimming Pool", "Dinner Restaurant"]
    room_types_ = ["Regular", "Suite", "Chalet"]
    room_category = ["Single", "Double", "Triple"]
    event_names = ["Hamaqi", "Amr Diab", "Wegz"]
    nations = ["Egypt", "USA", "UAE", "UK", "Germany", "Italy", "Portugal", "South Africa", "Spain", "France", "Russia", "Ukraine", "Belarus", "Sudan", "Sweden", "Switzerland", "Palestine", "Kuwait", "China", "India"]
    transaction_descriptions = ["Restaurant Purchase", "Yacht Ticket", "Bar Purchase"]
    event_types = ["Wedding", "Conference", "Concert", "Other"]

    get_random_name = lambda: f"{random.choice(all_names)} {random.choice(male_names)}"


    # Facilities
    cursor.execute("SET IDENTITY_INSERT Facilities ON")
    for i in range(len(facilities)):
        hh, dur = random.randint(8, 18), random.randint(1, 3)
        cursor.execute("INSERT INTO Facilities (FacilityID, FacilityName, FacilityWorkStart, FacilityWorkEnd) VALUES (?, ?, ?, ?)", i + 1, facilities[i], f"{hh}:00", f"{hh + dur}:00")
        cursor.commit()
    cursor.execute("SET IDENTITY_INSERT Facilities OFF")


    # Rooms
    room_types = {}
    cursor.execute("SET IDENTITY_INSERT Rooms ON")
    for i in range(BUILDING_NO):
        for j in range(FLOOR_NO):
            for k in range(ROOM_NO):
                room_type = random.choice(room_category)
                room_types[int(f"{i + 1}{j + 1}{(k + 1):02d}")] = room_type
                cursor.execute("INSERT INTO Rooms (RoomID, RoomBuildingNumber, RoomCategory, RoomType) VALUES (?, ?, ?, ?)", int(f"{i + 1}{j + 1}{(k + 1):02d}"), i + 1, room_type, random.choice(room_types_))
                cursor.commit()                
    cursor.execute("SET IDENTITY_INSERT Rooms OFF")


    # Employees
    EMPLOYEES = []
    for i in range(EMPLOYEE_NO):
        try:
            ssn = str(get_random_number(14))
            EMPLOYEES.append(ssn)
            cursor.execute("INSERT INTO Employees (EmployeeID, EmployeeName, EmployeeSalary, EmployeeFacilityFacilityID, WorkingHours) VALUES (?, ?, ?, ?, ?)", ssn, get_random_name(), random.randint(3, 14) * 1000, random.randint(1, len(facilities)), random.randint(2, 8))
            cursor.commit()
        except:
            i -= 1
            continue



    # Accounts
    
    emp1 = random.choice(EMPLOYEES)
    emps = {emp1}
    cursor.execute("SET IDENTITY_INSERT Accounts ON")
    cursor.execute("INSERT INTO Accounts (AccountID, AccountEmployeeEmployeeID, Password, Type) VALUES (?, ?, ?, ?)", 1, emp1, str(bcrypt.hashpw("test".encode(), bcrypt.gensalt()))[2:-1], "Manager")
    cursor.commit()
    for i in range(3):
        try:
            emp = random.choice(EMPLOYEES)
            if emp not in emps:
                cursor.execute("INSERT INTO Accounts (AccountID, AccountEmployeeEmployeeID, Password, Type) VALUES (?, ?, ?, ?)", i + 2, emp, str(bcrypt.hashpw("test".encode(), bcrypt.gensalt()))[2:-1], "Receptionist")
                cursor.commit()
                emps.add(emp)
        except:
            continue
    for i in range(2):
        try:
            emp = random.choice(EMPLOYEES)
            if emp not in emps:
                cursor.execute("INSERT INTO Accounts (AccountID, AccountEmployeeEmployeeID, Password, Type) VALUES (?, ?, ?, ?)", i + 5, emp, str(bcrypt.hashpw("test".encode(), bcrypt.gensalt()))[2:-1], "Restaurant Manager")
                cursor.commit()
                emps.add(emp)
        except:
            continue
    cursor.execute("SET IDENTITY_INSERT Accounts OFF")


    # Events
    cursor.execute("SET IDENTITY_INSERT Events ON")
    for i in range(EVENTS_NO):
        dstart = get_random_datetime()
        dend = dstart + datetime.timedelta(hours=random.randint(1, 3))
        dstart = dt_to_str(dstart)
        dend = dt_to_str(dend)
        cursor.execute("INSERT INTO Events (EventID, EventName, EventFee, EventStart, EventEnd, EventFacilityFacilityID, EventType) VALUES (?, ?, ?, ?, ?, ?, ?)", i + 1, random.choice(event_names), random.randrange(50000, 400001, 500), dstart, dend, random.randint(1, len(facilities)), random.choice(event_types))
        cursor.commit()
    cursor.execute("SET IDENTITY_INSERT Events OFF")
    cursor.commit()


    # Guests
    GUESTS = []
    for i in range(1, GUEST_MAX_COUNT + 1):
        try:
            ssn = str(get_random_number(14))
            GUESTS.append(ssn)
            cursor.execute("INSERT INTO Guests (GuestID, GuestName, GuestNationality, GuestPhoneNumber) VALUES (?, ?, ?, ?)", ssn, get_random_name(), random.choice(nations), get_random_number(11))
            cursor.commit()     
        except:
            i -= 1
            continue

    


    # Bookings + Transactions
    cursor.execute("SET IDENTITY_INSERT Bookings ON")
    c = 1
    for i in range(BUILDING_NO):
        for j in range(FLOOR_NO):
            for k in range(ROOM_NO):
                dstart = get_random_datetime()
                delta_d = random.randint(4, 15)
                dend = dstart + datetime.timedelta(days=delta_d)
                #dstart = dt_to_str(dstart)
                #dend = dt_to_str(dend)
                roomId = int(f"{i + 1}{j + 1}{(k + 1):02d}")

                
                cursor.execute("INSERT INTO Bookings (BookingID, CheckIn, CheckOut, BookingRoomRoomID, BookingGuestGuestID) VALUES (?, ?, ?, ?, ?)", c, dstart, dend, roomId, random.choice(GUESTS))
                cursor.commit()
                cursor.execute("INSERT INTO Transactions (TransactionDescription, TransactionFee, TransactionTime, TransactionRoomRoomID) VALUES (?, ?, ?, ?)", "Booking Fee", random.randint(5000, 25000), dstart, roomId)
                cursor.commit()

                for _ in range(random.randint(0, 4)):
                    dtransaction = dstart + datetime.timedelta(days=random.randint(0, delta_d - 1))
                    cursor.execute("INSERT INTO Transactions (TransactionDescription, TransactionFee, TransactionTime, TransactionRoomRoomID) VALUES (?, ?, ?, ?)", random.choice(transaction_descriptions), random.randint(20, 10000), dtransaction, roomId)
                    cursor.commit()

                c += 1

    cursor.execute("SET IDENTITY_INSERT Bookings OFF")


    print("Database Filled Successfully")
    time.sleep(3)
    
def get_random_number(n: int) -> int:

    num = ""
    for _ in range(n):
        num += str(random.randint(0, 9))
    return int(num)



def delete_all(cursor: pyodbc.Cursor) -> None:
    tables = ["Facilities", "Guests", "Rooms", "Transactions", "Events", "Employees", "Accounts", "Bookings"]
    for table in tables:
        cursor.execute(f"DELETE FROM {table}")
        cursor.commit()
    

def dt_to_str(dt: datetime.datetime) -> str:
    return dt.strftime("%Y-%m-%d %H:%M")


def get_random_datetime() -> datetime.datetime:
    start_date = datetime.datetime(2020, 1, 1)
    end_date = datetime.datetime(2023, 12, 31)
    time_between_dates = end_date - start_date
    days_between_dates = time_between_dates.days
    random_number_of_days = random.randrange(days_between_dates)
    random_date = start_date + datetime.timedelta(days=random_number_of_days)
    return random_date


def get_random_roomId() -> int:
    building = random.randint(1, BUILDING_NO)
    floor = random.randint(1, FLOOR_NO)
    room = random.randint(1, ROOM_NO)
    return int(f"{building}{floor}{room:02d}")


if __name__ == "__main__":
    main()