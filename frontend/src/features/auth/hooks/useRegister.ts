import { useState, useCallback } from 'react';
import { apiRegister } from '../api/auth.api';
import { RegisterFormState, RegisterStep, UserRole } from '../types/auth.types';

// ─── Error shape covers every field ────────────────────────────────────────────
interface RegisterErrors {
    firstName?: string;
    lastName?: string;
    username?: string;
    email?: string;
    phone?: string;
    address?: string;
    shopName?: string;
    verificationDoc?: string;
    password?: string;
    confirmPassword?: string;
    general?: string;
}

export const PASSWORD_RULES = [
    { label: 'At least 8 characters', test: (p: string) => p.length >= 8 },
    { label: 'One uppercase letter', test: (p: string) => /[A-Z]/.test(p) },
    { label: 'One number', test: (p: string) => /\d/.test(p) },
    { label: 'One special character', test: (p: string) => /[!@#$%^&*]/.test(p) },
];

const INITIAL_FORM: RegisterFormState = {
    role: 'customer',
    firstName: '',
    lastName: '',
    username: '',
    email: '',
    phone: '',
    password: '',
    confirmPassword: '',
    profilePhoto: null,
    address: '',
    shopName: '',
    shopPhoto: null,
    verificationDoc: null,
};

export function useRegister() {
    const [step, setStep] = useState<RegisterStep>('select-role');
    const [loading, setLoading] = useState(false);
    const [errors, setErrors] = useState<RegisterErrors>({});
    const [form, setForm] = useState<RegisterFormState>(INITIAL_FORM);
    const [agreed, setAgreed] = useState(false);

    // ── Generic field updater ──────────────────────────────────────────────────
    const updateField = useCallback(
        (name: keyof RegisterFormState, value: string | File | null) => {
            setForm((prev) => ({ ...prev, [name]: value }));
            if (errors[name as keyof RegisterErrors]) {
                setErrors((prev) => ({ ...prev, [name]: undefined }));
            }
        },
        [errors]
    );

    const clearFieldError = useCallback((field: keyof RegisterErrors) => {
        setErrors((prev) => ({ ...prev, [field]: undefined }));
    }, []);

    // ── Role selection (step 0 → 1) ────────────────────────────────────────────
    const selectRole = useCallback((role: UserRole) => {
        setForm((prev) => ({ ...prev, role }));
        setStep('personal-info');
    }, []);

    // ── Step 1 validation: Personal Info ──────────────────────────────────────
    const validatePersonalInfo = (): boolean => {
        const e: RegisterErrors = {};
        if (!form.firstName.trim()) e.firstName = 'First name is required.';
        if (!form.lastName.trim()) e.lastName = 'Last name is required.';
        if (!form.username.trim()) e.username = 'Username is required.';
        else if (!/^[a-zA-Z0-9_]{3,20}$/.test(form.username))
            e.username = 'Username must be 3–20 characters (letters, numbers, _).';
        if (!form.email) e.email = 'Email is required.';
        else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email))
            e.email = 'Enter a valid email address.';
        if (form.phone && !/^[\d\s\-()]{4,14}$/.test(form.phone))
            e.phone = 'Enter a valid local phone number.';
        setErrors(e);
        return Object.keys(e).length === 0;
    };

    const goToRoleDetails = useCallback(() => {
        if (validatePersonalInfo()) setStep('role-details');
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [form]);

    // ── Step 2 validation: Role-specific details ───────────────────────────────
    const validateRoleDetails = (): boolean => {
        const e: RegisterErrors = {};
        if (form.role === 'customer') {
            if (!form.address.trim()) e.address = 'Address is required.';
        } else {
            if (!form.shopName.trim()) e.shopName = 'Shop name is required.';
            if (!form.verificationDoc) e.verificationDoc = 'Verification document is required.';
        }
        setErrors(e);
        return Object.keys(e).length === 0;
    };

    const goToPasswordStep = useCallback(() => {
        if (validateRoleDetails()) setStep('set-password');
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [form]);

    // ── Step 3 validation: Password ────────────────────────────────────────────
    const validatePassword = (): boolean => {
        const e: RegisterErrors = {};
        if (!form.password) e.password = 'Password is required.';
        else if (form.password.length < 8) e.password = 'Password must be at least 8 characters.';
        if (!form.confirmPassword) e.confirmPassword = 'Please confirm your password.';
        else if (form.password !== form.confirmPassword)
            e.confirmPassword = 'Passwords do not match.';
        if (!agreed) e.general = 'You must agree to the Terms of Service.';
        setErrors(e);
        return Object.keys(e).length === 0;
    };

    // ── Submit registration ────────────────────────────────────────────────────
    const submitRegister = useCallback(async () => {
        if (!validatePassword()) return;
        setLoading(true);
        setErrors({});
        try {
            const res = await apiRegister(form);
            if (res.success) {
                setStep('verify-otp');
            } else {
                setErrors({ general: res.message });
            }
        } catch {
            setErrors({ general: 'Something went wrong. Please try again.' });
        } finally {
            setLoading(false);
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [form, agreed]);

    const passwordStrength = PASSWORD_RULES.filter((r) => r.test(form.password)).length;

    return {
        step,
        setStep,
        form,
        errors,
        setErrors,
        loading,
        agreed,
        setAgreed,
        updateField,
        clearFieldError,
        selectRole,
        goToRoleDetails,
        goToPasswordStep,
        submitRegister,
        passwordStrength,
        PASSWORD_RULES,
    };
}
