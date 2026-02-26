-- =============================================
-- SQL Script: Tạo bảng tranh chấp (Dispute)
-- Database: PostgreSQL (sportbicyclestore_db)
-- Chạy script này trong DBeaver
-- =============================================

-- Bảng tranh chấp chính
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

-- Bảng bằng chứng tranh chấp
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
