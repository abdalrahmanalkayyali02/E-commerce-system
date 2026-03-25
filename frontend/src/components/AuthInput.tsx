import React, { useState, InputHTMLAttributes } from 'react';
import './AuthInput.css';

interface AuthInputProps extends InputHTMLAttributes<HTMLInputElement> {
    label: string;
    icon: React.ReactNode;
    error?: string;
    rightElement?: React.ReactNode;
}

const AuthInput: React.FC<AuthInputProps> = ({
    label,
    icon,
    error,
    rightElement,
    id,
    ...rest
}) => {
    const [focused, setFocused] = useState(false);

    return (
        <div className={`auth-input-wrap ${error ? 'auth-input-wrap--error' : ''} ${focused ? 'auth-input-wrap--focused' : ''}`}>
            <label className="auth-input__label" htmlFor={id}>{label}</label>
            <div className="auth-input__field">
                <span className="auth-input__icon">{icon}</span>
                <input
                    id={id}
                    className="auth-input__el"
                    onFocus={() => setFocused(true)}
                    onBlur={() => setFocused(false)}
                    {...rest}
                />
                {rightElement && <span className="auth-input__right">{rightElement}</span>}
            </div>
            {error && <p className="auth-input__error">{error}</p>}
        </div>
    );
};

export default AuthInput;
