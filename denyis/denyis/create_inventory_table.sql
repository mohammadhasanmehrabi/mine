-- ساخت جدول انبار
CREATE TABLE IF NOT EXISTS inventory (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_name VARCHAR(255) NOT NULL,
    sku VARCHAR(100) NOT NULL UNIQUE,
    category VARCHAR(100) NOT NULL,
    quantity INT NOT NULL DEFAULT 0,
    min_stock INT NOT NULL DEFAULT 0,
    unit_price DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    total_price DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    supplier VARCHAR(255),
    supplier_contact VARCHAR(255),
    purchase_date DATE NOT NULL,
    stock_status ENUM('موجود', 'کم موجود', 'ناموجود') DEFAULT 'موجود',
    notes TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- اضافه کردن ایندکس برای جستجوی بهتر
CREATE INDEX idx_inventory_product_name ON inventory(product_name);
CREATE INDEX idx_inventory_sku ON inventory(sku);
CREATE INDEX idx_inventory_category ON inventory(category);
CREATE INDEX idx_inventory_stock_status ON inventory(stock_status);
CREATE INDEX idx_inventory_low_stock ON inventory(quantity, min_stock); 