USE master;
GO

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'appointmentscheduler')
BEGIN
    CREATE DATABASE appointmentscheduler;
END
GO

USE appointmentscheduler;
GO

-- Rest of your script for creating tables and inserting data


CREATE TABLE roles (
    id INT IDENTITY(1,1) NOT NULL,
    name VARCHAR(50) NULL,
    CONSTRAINT PK_roles PRIMARY KEY (id)
);

-- Create 'users' table
CREATE TABLE users (
    id INT IDENTITY(1,1) NOT NULL,
    username VARCHAR(50) NOT NULL,
    password CHAR(80) NOT NULL,
    first_name NVARCHAR(50),
    last_name NVARCHAR(50),
    email VARCHAR(50),
    mobile VARCHAR(50),
    street NVARCHAR(50),
    city NVARCHAR(50),
    CONSTRAINT PK_users PRIMARY KEY (id)
);


-- Create 'users_roles' table
CREATE TABLE users_roles (
    user_id INT NOT NULL,
    role_id INT NOT NULL,
    CONSTRAINT PK_users_roles PRIMARY KEY (user_id, role_id),
    CONSTRAINT FK_users_user FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_roles_role FOREIGN KEY (role_id) REFERENCES roles (id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- Create 'works' table
CREATE TABLE works (
    id INT IDENTITY(1,1) NOT NULL,
    name NVARCHAR(256),
    duration INT,
    price DECIMAL(10, 2),
    editable BIT,
    target VARCHAR(256),
    description NVARCHAR(256),
    CONSTRAINT PK_works PRIMARY KEY (id)
);

-- Create 'invoices' table
CREATE TABLE invoices (
    id INT IDENTITY(1,1) NOT NULL,
    number VARCHAR(256),
    status VARCHAR(256),
    total_amount DECIMAL(10, 2),
    issued DATETIME,
    CONSTRAINT PK_invoices PRIMARY KEY (id)
);

-- sql server hạn chế về multiple cascade paths nên thay thế bằng no action
CREATE TABLE appointments (
    id INT IDENTITY(1,1) NOT NULL,
    [start] DATETIME,
    [end] DATETIME,
    canceled_at DATETIME,
    [status] VARCHAR(20),
    id_canceler INT,
    id_provider INT,
    id_customer INT,
    id_work INT,
    id_invoice INT,
    CONSTRAINT PK_appointments PRIMARY KEY (id),
    CONSTRAINT FK_appointments_users_canceler FOREIGN KEY (id_canceler) REFERENCES users (id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_appointments_users_customer FOREIGN KEY (id_customer) REFERENCES users (id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_appointments_works FOREIGN KEY (id_work) REFERENCES works (id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_appointments_users_provider FOREIGN KEY (id_provider) REFERENCES users (id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_appointments_invoices FOREIGN KEY (id_invoice) REFERENCES invoices (id) ON DELETE NO ACTION ON UPDATE NO ACTION
);


-- works_providers
CREATE TABLE works_providers (
    id_user INT NOT NULL,
    id_work INT NOT NULL,
    CONSTRAINT PK_works_providers PRIMARY KEY (id_user, id_work),
    CONSTRAINT FK_works_providers_users_provider FOREIGN KEY (id_user) REFERENCES users (id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_works_providers_works FOREIGN KEY (id_work) REFERENCES works (id) ON DELETE CASCADE ON UPDATE CASCADE
);

-- working_plans
CREATE TABLE working_plans (
    id_provider INT NOT NULL PRIMARY KEY,
    monday TEXT,
    tuesday TEXT,
    wednesday TEXT,
    thursday TEXT,
    friday TEXT,
    saturday TEXT,
    sunday TEXT,
    CONSTRAINT FK_working_plans_provider FOREIGN KEY (id_provider) REFERENCES users (id) ON DELETE NO ACTION ON UPDATE NO ACTION
);


-- messages
CREATE TABLE messages (
    id INT IDENTITY(1,1) NOT NULL,
    created_at DATETIME,
    [message] TEXT,
    id_author INT,
    id_appointment INT,
    CONSTRAINT PK_messages PRIMARY KEY (id),
    CONSTRAINT FK_messages_author FOREIGN KEY (id_author) REFERENCES users (id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_messages_appointment FOREIGN KEY (id_appointment) REFERENCES appointments (id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- corporate_customers
CREATE TABLE corporate_customers (
    id_customer INT NOT NULL PRIMARY KEY,
    vat_number VARCHAR(256),
    company_name NVARCHAR(256),
    CONSTRAINT FK_corporate_customer_user FOREIGN KEY (id_customer) REFERENCES users (id)
);

-- providers
CREATE TABLE providers (
    id_provider INT NOT NULL PRIMARY KEY,
    CONSTRAINT FK_provider_user FOREIGN KEY (id_provider) REFERENCES users (id)
);


-- retail_customers
CREATE TABLE retail_customers (
    id_customer INT NOT NULL PRIMARY KEY,
    CONSTRAINT FK_retail_customer_user FOREIGN KEY (id_customer) REFERENCES users (id)
);

-- customers
CREATE TABLE customers (
    id_customer INT NOT NULL PRIMARY KEY,
    CONSTRAINT FK_customer_user FOREIGN KEY (id_customer) REFERENCES users (id)
);

-- notifications
CREATE TABLE notifications (
    id INT IDENTITY(1,1) NOT NULL,
    title NVARCHAR(256),
    [message] NVARCHAR(256),
    created_at DATETIME,
    url VARCHAR(256),
    is_read BIT,
    id_user INT,
    CONSTRAINT PK_notifications PRIMARY KEY (id),
    CONSTRAINT FK_notifications_user FOREIGN KEY (id_user) REFERENCES users (id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- exchanges
CREATE TABLE exchanges (
    id INT IDENTITY(1,1) NOT NULL,
    exchange_status VARCHAR(256),
    id_appointment_requestor INT,
    id_appointment_requested INT,
    CONSTRAINT PK_exchanges PRIMARY KEY (id),
    CONSTRAINT FK_exchange_appointment_requestor FOREIGN KEY (id_appointment_requestor) REFERENCES appointments (id) ON DELETE NO ACTION ON UPDATE NO ACTION,
    CONSTRAINT FK_exchange_appointment_requested FOREIGN KEY (id_appointment_requested) REFERENCES appointments (id) ON DELETE NO ACTION ON UPDATE NO ACTION
);

-- INSERT available roles
INSERT INTO roles (name) VALUES ('ROLE_ADMIN'),
                                   ('ROLE_PROVIDER'),
                                   ('ROLE_CUSTOMER'),
                                   ('ROLE_CUSTOMER_CORPORATE'),
                                   ('ROLE_CUSTOMER_RETAIL');

-- INSERT admin account with username: 'admin' and password 'qwerty123'
INSERT INTO users (username, password)
VALUES ('admin', 'qwerty123');
INSERT INTO users_roles (user_id, role_id)
VALUES (1, 1);

-- INSERT provider account with username: 'provider' and password 'qwerty123'
INSERT INTO users (username, password)
VALUES ('provider', 'qwerty123');
INSERT INTO providers (id_provider)
VALUES (2);
INSERT INTO users_roles (user_id, role_id)
VALUES (2, 2);

-- INSERT retail customer account with username: 'customer_r' and password 'qwerty123'
INSERT INTO users (username, password)
VALUES ('customer_r', 'qwerty123');
INSERT INTO customers (id_customer)
VALUES (3);
INSERT INTO retail_customers (id_customer)
VALUES (3);
INSERT INTO users_roles (user_id, role_id)
VALUES (3, 3);
INSERT INTO users_roles (user_id, role_id)
VALUES (3, 5);

-- INSERT corporate customer account with username: 'customer_c' and password 'qwerty123'
INSERT INTO users (username, password)
VALUES ('customer_c', 'qwerty123');

INSERT INTO customers (id_customer)
VALUES (4);

INSERT INTO corporate_customers (id_customer, vat_number, company_name)
VALUES (4, '123456789', N'Công ty trách nhiệm hữu hạn 1 thành viên');

INSERT INTO users_roles (user_id, role_id)
VALUES (4, 3);

INSERT INTO users_roles (user_id, role_id)
VALUES (4, 4);

INSERT INTO works (name, duration, price, editable, target, description)
VALUES (N'Lớp cô giáo Thảo', 60, 100000, 1, 'retail',
        N'Lớp học tiếng anh kéo dài 60 phút và giá 100k một buổi');

INSERT INTO works_providers
VALUES (2, 1);


INSERT INTO working_plans (id_provider, monday, tuesday, wednesday, thursday, friday, saturday, sunday)
VALUES (2,
        '{"workingHours":{"start":[6,0],"end":[18,0]},"breaks":[]}',
        '{"workingHours":{"start":[6,0],"end":[18,0]},"breaks":[]}',
        '{"workingHours":{"start":[6,0],"end":[18,0]},"breaks":[]}',
        '{"workingHours":{"start":[6,0],"end":[18,0]},"breaks":[]}',
        '{"workingHours":{"start":[6,0],"end":[18,0]},"breaks":[]}',
        '{"workingHours":{"start":[6,0],"end":[18,0]},"breaks":[]}',
        '{"workingHours":{"start":[6,0],"end":[18,0]},"breaks":[]}');
GO

-- Thêm dữ liệu cho bảng 'works'
INSERT INTO works (name, duration, price, editable, target, description)
VALUES 
    (N'Lớp học piano', 45, 150000, 1, 'retail', N'Lớp học piano dành cho người mới bắt đầu'),
    (N'Massages', 60, 200000, 1, 'retail', N'Dịch vụ massage toàn diện'),
    (N'Xây dựng website', 120, 500000, 1, 'corporate', N'Thiết kế và xây dựng website cho doanh nghiệp');
GO

-- Thêm dữ liệu cho bảng 'works_providers'
INSERT INTO works_providers (id_user, id_work)
VALUES 
    (2, 2),
    (2, 3),
    (2, 4);
GO

-- Thêm dữ liệu cho bảng 'appointments'
INSERT INTO appointments ([start], [end], canceled_at, [status], id_canceler, id_provider, id_customer, id_work, id_invoice)
VALUES 
    ('2024-03-01 10:00:00', '2024-03-01 11:00:00', NULL, 'Scheduled', NULL, 2, 3, 1, NULL),
    ('2024-03-02 14:30:00', '2024-03-02 15:30:00', NULL, 'Scheduled', NULL, 2, 4, 1, NULL),
    ('2024-03-03 16:00:00', '2024-03-03 17:00:00', NULL, 'Scheduled', NULL, 2, 4, 1, NULL);
	GO

	-- Thêm dữ liệu cho bảng 'notifications'
INSERT INTO notifications (title, [message], created_at, url, is_read, id_user)
VALUES 
    (N'Thông báo mới', N'Có một thông báo mới từ hệ thống.', GETDATE(), '/notifications/1', 0, 2),
    (N'Nhắc nhở', N'Hôm nay bạn có buổi học lớp piano.', GETDATE(), '/notifications/2', 0, 3),
    (N'Đặt hẹn thành công', N'Bạn đã đặt lịch hẹn thành công.', GETDATE(), '/notifications/3', 0, 4),
    (N'Thông báo hủy', N'Lịch hẹn của bạn đã bị hủy bởi nhà cung cấp.', GETDATE(), '/notifications/4', 0, 3),
    (N'Thông báo thanh toán', N'Bạn có một hóa đơn mới cần thanh toán.', GETDATE(), '/notifications/5', 0, 3);
GO

-- Tạo bảng và thêm dữ liệu cho 'messages'
INSERT INTO messages (created_at, [message], id_author, id_appointment)
VALUES (GETDATE(), N'Chào anh/chị, hẹn gặp lại vào buổi học tiếp theo.', 2, 1);

-- Thêm dữ liệu cho bảng 'users'
INSERT INTO users (username, password, first_name, last_name, email, mobile, street, city)
VALUES 
    ('user1', 'password1', N'Nguyễn', N'Văn A', 'user1@example.com', '123456789', N'Tân Bình', N'TP.Hồ Chí Minh'),
    ('user2', 'password2', N'Nguyễn', N'Thị B', 'user2@example.com', '987654321', N'Tân Phú', N'TP.Hồ Chí Minh'),
    ('user3', 'password3', N'Phạm', N'Thị C', 'user3@example.com', '555555555', N'Quận 1', N'TP.Hồ Chí Minh'),
    ('user4', 'password4', N'Trần', N'Văn D', 'user4@example.com', '111111111', N'Quận 7', N'TP.Hồ Chí Minh'),
    ('user5', 'password5', N'Lê', N'Thị E', 'user5@example.com', '222222222', N'Bình Thạnh', N'TP.Hồ Chí Minh'),
    ('user6', 'password6', N'Hồ', N'Trung F', 'user6@example.com', '333333333', N'Gò Vấp', N'TP.Hồ Chí Minh'),
    ('user7', 'password7', N'Đặng', N'Thị G', 'user7@example.com', '444444444', N'Tân Phú', N'TP.Hồ Chí Minh'),
    ('user8', 'password8', N'Phan', N'Văn H', 'user8@example.com', '555555555', N'Tân Bình', N'TP.Hồ Chí Minh'),
    ('user9', 'password9', N'Trịnh', N'Thị I', 'user9@example.com', '666666666', N'Quận 10', N'TP.Hồ Chí Minh'),
    ('user10', 'password10', N'Vũ', N'Trung K', 'user10@example.com', '777777777', N'Quận 3', N'TP.Hồ Chí Minh');
GO
-- Thêm dữ liệu cho bảng 'providers'
INSERT INTO providers (id_provider)
VALUES (5),
	(6),
	(7),
	(8);
GO
-- Thêm dữ liệu cho bảng 'users_roles'
INSERT INTO users_roles (user_id, role_id)
VALUES (5, 3),
		(6, 3),
		(7, 3),
		(8, 3),
		(9, 3),
		(10, 3),
		(11, 3),
		(5, 4),
		(6, 4),
		(7, 4),
		(8, 4),
		(9, 5),
		(10, 5),
		(11, 5);
GO