
import React, { useState, useContext, FormEvent } from "react";
import ReactDOM from 'react-dom';
import Card from 'react-bootstrap/Card';
import { Form, Alert } from "react-bootstrap";
import Button from 'react-bootstrap/Button';
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../context/AuthContext";
import  style  from "./Login.module.scss";
import api from "../../services/Api";


export default function AppLogin() {
  const [inputUsername, setInputUsername] = useState("");
  const [inputPassword, setInputPassword] = useState("");

  const [loading, setLoading] = useState(false);
  const [show, setShow] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const navigate = useNavigate();
  const authContext = useContext(AuthContext);

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setLoading(true);
    try {
      const loginData = {
        username: inputUsername,
        password: inputPassword,
      };

      const response = await api.post("/api/auth/login", loginData);
      const { token } = response.data;

      if (authContext) {
        authContext.login(token);
      }

      setLoading(true);
      await delay(500);
      navigate("/AppHome");
    } catch (error) {
      setLoading(false);
      setShow(true);
      setErrorMessage("Falha na autenticação. Por favor, verifique suas credenciais.");
    }
  };

  function delay(ms: number): Promise<void> {
    return new Promise((resolve) => setTimeout(resolve, ms));
  }

  return (

    <div className={style.AppStyle}>

    {/* // shadow p-3 mb-5 bg-white rounded
    // d-flex justify-content-center
    // d-flex flex-column justify-content-center align-items-center */}

{/* "shadow p-3 d-flex flex-column col-md-6 offset-md-3 justify-content-center  align-self-center bg-gray  rounded " */}

    <p className={style.TituloPrincipal}>Students App</p>
    <p className={style.SubTitulo}>Seja bem-vindo!</p>
    
    <div className={"shadow p-3 d-flex  col-md-5 justify-content-center  align-self-center bg-light rounded"}>

    
         
    {/* container-login form */}
      <Form className={""} onSubmit={handleSubmit}>

        <p className={style.Titulo}>Faça seu login</p>

        <Form.Group className={"mb-4"} controlId="username">
          <Form.Label>Usuário: </Form.Label>
          <Form.Control
            type="text"
            placeholder="Digite seu usuário"
            value={inputUsername}
            onChange={(e) => setInputUsername(e.target.value)}
            required
          />
        </Form.Group>

        <Form.Group className={"mb-5"} controlId="password">
          <Form.Label>Senha: </Form.Label>
          <Form.Control
            type="password"
            placeholder="Digite sua senha"
            value={inputPassword}
            onChange={(e) => setInputPassword(e.target.value)}
            required
          />
        </Form.Group>

            {/* mb-2 */}
        {show ? (
          <Alert
            className={"mx-auto"}
            variant="danger"
            onClose={() => setShow(false)}
            dismissible
          >
            Usuário e/ou Senha incorreto(s).
          </Alert>
        ) : (
          <div />
        )}

        <Form.Group className={"d-grid gap-2 d-md-flex justify-content-md-center"}>
          {!loading ? (
            <Button className={style.btn} variant="dark" type="submit">
              Logar
            </Button>
          ) : (
            <Button className={style.btn} variant="dark" type="submit" disabled>
              Logando...
            </Button>
          )}
        </Form.Group>


      </Form>
     
      
    </div>
    </div>
  );
}