// ─── Auth Feature — Public API ────────────────────────────────────────────────
// Import from this barrel instead of direct internal paths

// Pages
export { default as LoginPage } from './pages/LoginPage';
export { default as RegisterPage } from './pages/RegisterPage';
export { default as ForgotPasswordPage } from './pages/ForgotPasswordPage';
export { default as OtpVerifyPage } from './pages/OtpVerifyPage';
export { default as SuccessPage } from './pages/SuccessPage';

// Hooks
export { useLogin } from './hooks/useLogin';
export { useRegister } from './hooks/useRegister';
export { useOtp } from './hooks/useOtp';
export { useForgotPassword } from './hooks/useForgotPassword';

// API
export * from './api/auth.api';

// Types
export * from './types/auth.types';
