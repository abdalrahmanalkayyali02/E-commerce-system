import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { Mail, Lock, Eye, EyeOff, ArrowRight, Loader2 } from 'lucide-react';
import AuthLayout from '../../../pages/auth/AuthLayout';
import AuthInput from '../../../components/AuthInput';
import { useLogin } from '../hooks/useLogin';
// shared styles
import '../../../pages/auth/LoginPage.css';

const LoginPage: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [showPassword, setShowPassword] = useState(false);
    const [rememberMe, setRememberMe] = useState(false);
    const { login, loading, errors, clearFieldError } = useLogin();

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        login({ email, password });
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
                            value={email}
                            onChange={(e) => {
                                setEmail(e.target.value);
                                clearFieldError('email');
                            }}
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
                            value={password}
                            onChange={(e) => {
                                setPassword(e.target.value);
                                clearFieldError('password');
                            }}
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
