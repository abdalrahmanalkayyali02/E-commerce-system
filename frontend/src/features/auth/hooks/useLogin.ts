import { useState, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { apiLogin } from '../api/auth.api';
import { LoginCredentials } from '../types/auth.types';

interface LoginErrors {
    email?: string;
    password?: string;
    general?: string;
}

export function useLogin() {
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [errors, setErrors] = useState<LoginErrors>({});

    const validate = (form: LoginCredentials): boolean => {
        const e: LoginErrors = {};
        if (!form.email) e.email = 'Email is required.';
        else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email))
            e.email = 'Enter a valid email address.';
        if (!form.password) e.password = 'Password is required.';
        else if (form.password.length < 6) e.password = 'Password must be at least 6 characters.';
        setErrors(e);
        return Object.keys(e).length === 0;
    };

    const clearFieldError = useCallback((field: keyof LoginErrors) => {
        setErrors((prev) => ({ ...prev, [field]: undefined }));
    }, []);

    const login = useCallback(
        async (form: LoginCredentials) => {
            if (!validate(form)) return;
            setLoading(true);
            setErrors({});
            try {
                const res = await apiLogin(form);
                if (res.success) {
                    navigate('/dashboard');
                } else {
                    setErrors({ general: res.message });
                }
            } catch {
                setErrors({ general: 'Something went wrong. Please try again.' });
            } finally {
                setLoading(false);
            }
        },
        [navigate]
    );

    return { login, loading, errors, clearFieldError, setErrors };
}
