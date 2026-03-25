// ─── Auth Types ────────────────────────────────────────────────────────────────

export type UserRole = 'customer' | 'seller';

export interface LoginCredentials {
    email: string;
    password: string;
}

// ─── Shared registration fields ────────────────────────────────────────────────
interface RegisterBasePayload {
    role: UserRole;
    firstName: string;
    lastName: string;
    username: string;
    email: string;
    phone: string;
    password: string;
    confirmPassword: string;
    profilePhoto?: File | null;
}

// ─── Customer-specific fields ──────────────────────────────────────────────────
export interface CustomerRegisterPayload extends RegisterBasePayload {
    role: 'customer';
    address: string;
}

// ─── Seller-specific fields ────────────────────────────────────────────────────
export interface SellerRegisterPayload extends RegisterBasePayload {
    role: 'seller';
    shopName: string;
    shopPhoto?: File | null;
    /** Official document (trade licence, ID, etc.) proving seller identity */
    verificationDoc: File;
}

export type RegisterPayload = CustomerRegisterPayload | SellerRegisterPayload;

// ─── Form state (all fields present, role discriminates which are required) ────
export interface RegisterFormState {
    role: UserRole;
    firstName: string;
    lastName: string;
    username: string;
    email: string;
    phone: string;
    password: string;
    confirmPassword: string;
    profilePhoto: File | null;
    // Customer
    address: string;
    // Seller
    shopName: string;
    shopPhoto: File | null;
    /** Verification document — required for sellers */
    verificationDoc: File | null;
}

export interface OtpVerifyPayload {
    email: string;
    otp: string;
    /** Purpose of the OTP - registration or password reset */
    purpose: 'register' | 'forgot-password';
}

export interface ResetPasswordPayload {
    email: string;
    otp: string;
    newPassword: string;
    confirmPassword: string;
}

export interface AuthUser {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    username?: string;
    phone?: string;
    role: UserRole;
}

export interface AuthApiResponse<T = void> {
    success: boolean;
    message: string;
    data?: T;
}

// Flow states for multi-step auth
export type RegisterStep =
    | 'select-role'
    | 'personal-info'
    | 'role-details'
    | 'set-password'
    | 'verify-otp'
    | 'success';

export type ForgotPasswordStep = 'enter-email' | 'verify-otp' | 'set-new-password' | 'success';
