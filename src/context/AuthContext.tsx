import React, { createContext, useState, ReactNode } from 'react';

interface AuthContextType {
    authToken: string | null;
    login: (token: string) => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

interface AuthProviderProps {
    children: ReactNode;
}

const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
    const [authToken, setAuthToken] = useState<string | null>(localStorage.getItem('jwtToken'));

    const login = (token: string) => {
        localStorage.setItem('jwtToken', token);
        setAuthToken(token);
    };

    return (
        <AuthContext.Provider value= {{ authToken, login }
}>
    { children }
    </AuthContext.Provider>
  );
};

export { AuthContext, AuthProvider };