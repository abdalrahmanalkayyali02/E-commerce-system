import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Mail, Lock, Eye, EyeOff, ArrowRight, Loader2 } from 'lucide-react';
import AuthLayout from './AuthLayout';
import AuthInput from '../../components/AuthInput';
import './LoginPage.css';

interface FormState {
    email: string;
    password: string;
}
interface Errors {
    email?: string;
    password?: string;
    general?: string;
}

const LoginPage: React.FC = () => {
    const navigate = useNavigate();
    const [form, setForm] = useState<FormState>({ email: '', password: '' });
    const [errors, setErrors] = useState<Errors>({});
    const [showPassword, setShowPassword] = useState(false);
    const [loading, setLoading] = useState(false);
    const [rememberMe, setRememberMe] = useState(false);

    const validate = (): boolean => {
        const e: Errors = {};
        if (!form.email) e.email = 'Email is required.';
        else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email))
            e.email = 'Enter a valid email address.';
        if (!form.password) e.password = 'Password is required.';
        else if (form.password.length < 6) e.password = 'Password must be at least 6 characters.';
        setErrors(e);
        return Object.keys(e).length === 0;
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
        if (errors[name as keyof Errors]) {
            setErrors((prev) => ({ ...prev, [name]: undefined }));
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!validate()) return;
        setLoading(true);
        setErrors({});
        try {
            // Simulate API call
            await new Promise((res) => setTimeout(res, 1800));
            // On success navigate to dashboard
            navigate('/');
        } catch {
            setErrors({ general: 'Invalid credentials. Please try again.' });
        } finally {
            setLoading(false);
        }
    };

    return (
        <AuthLayout
            illustrationTitle="Shop Smarter, Live Better."
            illustrationSubtitle="Discover thousands of products with unbeatable deals, fast delivery & secure checkout."
        >
            <div className="login-page">
                {/* Header */}
                <div className="login-page__header">
                    <div className="login-page__greeting">👋 Welcome back</div>
                    <h2 className="login-page__title">Sign in to NexCart</h2>
                    <p className="login-page__subtitle">
                        Don't have an account?{' '}
                        <Link to="/register">Create one for free →</Link>
                    </p>
                </div>



                {/* General error */}
                {errors.general && (
                    <div className="login-page__alert" role="alert">
                        🚨 {errors.general}
                    </div>
                )}

                {/* Form */}
                <form id="login-form" onSubmit={handleSubmit} noValidate>
                    <div className="login-page__fields">
                        <AuthInput
                            id="login-email"
                            name="email"
                            type="email"
                            label="Email Address"
                            placeholder="you@example.com"
                            value={form.email}
                            onChange={handleChange}
                            error={errors.email}
                            autoComplete="email"
                            icon={<Mail size={17} />}
                        />
                        <AuthInput
                            id="login-password"
                            name="password"
                            type={showPassword ? 'text' : 'password'}
                            label="Password"
                            placeholder="Enter your password"
                            value={form.password}
                            onChange={handleChange}
                            error={errors.password}
                            autoComplete="current-password"
                            icon={<Lock size={17} />}
                            rightElement={
                                <span
                                    role="button"
                                    aria-label="Toggle password visibility"
                                    onClick={() => setShowPassword((v) => !v)}
                                >
                                    {showPassword ? <EyeOff size={16} /> : <Eye size={16} />}
                                </span>
                            }
                        />
                    </div>

                    {/* Remember + Forgot */}
                    <div className="login-page__options">
                        <label className="login-page__remember" htmlFor="remember-me">
                            <input
                                id="remember-me"
                                type="checkbox"
                                checked={rememberMe}
                                onChange={(e) => setRememberMe(e.target.checked)}
                            />
                            <span className="checkmark" />
                            Remember me
                        </label>
                        <Link to="/forgot-password" className="login-page__forgot">
                            Forgot password?
                        </Link>
                    </div>

                    {/* Submit */}
                    <button
                        id="login-submit"
                        type="submit"
                        className={`login-page__submit ${loading ? 'loading' : ''}`}
                        disabled={loading}
                    >
                        {loading ? (
                            <>
                                <Loader2 size={18} className="spin" />
                                Signing in…
                            </>
                        ) : (
                            <>
                                Sign In
                                <ArrowRight size={18} />
                            </>
                        )}
                    </button>
                </form>

                {/* Terms */}
                <p className="login-page__terms">
                    By signing in, you agree to our{' '}
                    <Link to="/terms">Terms of Service</Link> and{' '}
                    <Link to="/privacy">Privacy Policy</Link>.
                </p>
            </div>
        </AuthLayout>
    );
};

export default LoginPage;
