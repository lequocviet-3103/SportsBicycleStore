using System;
using Npgsql;

var connStr = "Host=dpg-d619rrsoud1c739vkk20-a.singapore-postgres.render.com;Port=5432;Database=sportbicyclestore_db;Username=admin;Password=9JIgpwTbCTtygGd4HydNmDldqDcyDeWc;SSL Mode=Require;Trust Server Certificate=true";

var sql = @"
CREATE TABLE IF NOT EXISTS mdispute (
    dispute_id VARCHAR(40) PRIMARY KEY,
    order_id VARCHAR(40) NOT NULL,
    buyer_id VARCHAR(40) NOT NULL,
    seller_id VARCHAR(40) NOT NULL,
    reason INTEGER NOT NULL,
    description TEXT NOT NULL,
    evidence_urls TEXT,
    status INTEGER NOT NULL DEFAULT 1,
    resolution INTEGER,
    refund_amount NUMERIC(18,2),
    resolved_by VARCHAR(40),
    admin_note TEXT,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    resolved_at TIMESTAMP WITHOUT TIME ZONE,
    CONSTRAINT fk_dispute_order FOREIGN KEY (order_id) REFERENCES morder(order_id),
    CONSTRAINT fk_dispute_buyer FOREIGN KEY (buyer_id) REFERENCES muser(user_id),
    CONSTRAINT fk_dispute_seller FOREIGN KEY (seller_id) REFERENCES muser(user_id),
    CONSTRAINT fk_dispute_resolver FOREIGN KEY (resolved_by) REFERENCES muser(user_id)
);

CREATE TABLE IF NOT EXISTS mdispute_evidence (
    evidence_id VARCHAR(40) PRIMARY KEY,
    dispute_id VARCHAR(40) NOT NULL,
    submitted_by VARCHAR(40) NOT NULL,
    image_url VARCHAR(500),
    video_url VARCHAR(500),
    description TEXT,
    created_at TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_evidence_dispute FOREIGN KEY (dispute_id) REFERENCES mdispute(dispute_id) ON DELETE CASCADE,
    CONSTRAINT fk_evidence_submitter FOREIGN KEY (submitted_by) REFERENCES muser(user_id)
);
";

try
{
    await using var conn = new NpgsqlConnection(connStr);
    await conn.OpenAsync();
    Console.WriteLine("Connected to database.");
    
    await using var cmd = new NpgsqlCommand(sql, conn);
    await cmd.ExecuteNonQueryAsync();
    Console.WriteLine("Tables created successfully!");

    // Check if test order exists
    var checkSql = "SELECT COUNT(*) FROM morder WHERE order_id = 'test-order-001'";
    await using var checkCmd = new NpgsqlCommand(checkSql, conn);
    var count = (long)(await checkCmd.ExecuteScalarAsync())!;
    
    if (count == 0)
    {
        // Insert test data for dispute testing
        var insertSql = @"
-- Insert test brand if not exists
INSERT INTO mbrand (brand_id, brand_name, description, is_active)
VALUES ('brand-001', 'Giant', 'Giant Bicycles', true)
ON CONFLICT (brand_id) DO NOTHING;

-- Insert test category if not exists
INSERT INTO mcategory (category_id, category_name, description, is_active)
VALUES ('cat-001', 'Mountain Bike', 'Mountain bikes', true)
ON CONFLICT (category_id) DO NOTHING;

-- Insert test product
INSERT INTO mproduct (product_id, seller_id, category_id, brand_id, product_name, description, condition, price, stock_quantity, status)
VALUES ('product-001', 'seller-001', 'cat-001', 'brand-001', 'Giant XTC SLR 29', 'Mountain bike 29 inch', 1, 15000000, 1, 1)
ON CONFLICT (product_id) DO NOTHING;

-- Insert test order with status Paid (3)
INSERT INTO morder (order_id, buyer_id, seller_id, total_amount, shipping_address, receiver_name, receiver_phone, delivery_method, order_status, payment_status, created_at, updated_at)
VALUES ('test-order-001', 'buyer-001', 'seller-001', 15000000, '123 Test Street, HCM', 'Nguyen Van Mua', '0901234567', 1, 3, 2, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
ON CONFLICT (order_id) DO NOTHING;

-- Insert order detail
INSERT INTO morderdetail (order_detail_id, order_id, product_id, quantity, unit_price, subtotal)
VALUES ('detail-001', 'test-order-001', 'product-001', 1, 15000000, 15000000)
ON CONFLICT (order_detail_id) DO NOTHING;

-- Insert another order with Completed status (4) for second test
INSERT INTO morder (order_id, buyer_id, seller_id, total_amount, shipping_address, receiver_name, receiver_phone, delivery_method, order_status, payment_status, created_at, updated_at)
VALUES ('test-order-002', 'buyer-001', 'seller-001', 15000000, '123 Test Street, HCM', 'Nguyen Van Mua', '0901234567', 1, 4, 2, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
ON CONFLICT (order_id) DO NOTHING;

INSERT INTO morderdetail (order_detail_id, order_id, product_id, quantity, unit_price, subtotal)
VALUES ('detail-002', 'test-order-002', 'product-001', 1, 15000000, 15000000)
ON CONFLICT (order_detail_id) DO NOTHING;
";
        await using var insertCmd = new NpgsqlCommand(insertSql, conn);
        await insertCmd.ExecuteNonQueryAsync();
        Console.WriteLine("Test data inserted successfully!");
    }
    else
    {
        Console.WriteLine("Test data already exists.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
