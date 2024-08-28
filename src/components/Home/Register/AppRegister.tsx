import React, { useContext, useState, useEffect } from "react";
import { AuthContext } from "../../../context/AuthContext";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Form from "react-bootstrap/Form";
import api from "../../../services/Api";
import { User } from "../Interface/User";

interface AppRegisterProps {
  getAllStudents: () => void;
  userToEdit?: User | null;
  onClose: () => void;
}
 
export default function AppRegister({
  getAllStudents,
  userToEdit,
  onClose,
}: AppRegisterProps) {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("AuthContext must be used within an AuthProvider");
  }

  const { authToken } = context;
  const [show, setShow] = useState(true);
  const [formData, setFormData] = useState({
    nome: "",
    idade: "",
    serie: "",
    notaMedia: "",
    endereco: "",
    nomePai: "",
    nomeMae: "",
    dataNascimento: "",
  });
  const [errors, setErrors] = useState({
    nome: false,
    idade: false,
    serie: false,
    notaMedia: false,
    endereco: false,
    nomePai: false,
    nomeMae: false,
    dataNascimento: false,
  });

  useEffect(() => {
    if (userToEdit) {
      setFormData({
        nome: userToEdit.nome || "",
        idade: userToEdit.idade.toString() || "",
        serie: userToEdit.serie.toString() || "",
        notaMedia: userToEdit.notaMedia.toString() || "",
        endereco: userToEdit.endereco || "",
        nomePai: userToEdit.nomePai || "",
        nomeMae: userToEdit.nomeMae || "",
        dataNascimento:
          typeof userToEdit.dataNascimento === "string"
            ? userToEdit.dataNascimento
            : userToEdit.dataNascimento instanceof Date
              ? userToEdit.dataNascimento.toISOString().split("T")[0]
              : "",
      });
    }
  }, [userToEdit]);

  const handleClose = () => {
    setShow(false);
    onClose();
  };

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setFormData({
      ...formData,
      [name]: value,
    });
    setErrors({
      ...errors,
      [name]: false,
    });
  };

  const validateForm = () => {
    const newErrors = {
      nome: !formData.nome,
      idade: !formData.idade,
      serie: !formData.serie,
      notaMedia: !formData.notaMedia,
      endereco: !formData.endereco,
      nomePai: !formData.nomePai,
      nomeMae: !formData.nomeMae,
      dataNascimento: !formData.dataNascimento,
    };
    setErrors(newErrors);
    return !Object.values(newErrors).includes(true);
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (validateForm()) {
      try {
        if (userToEdit) {
        
          await api.put(`/api/students/${userToEdit.id}`, formData, {
            headers: {
              Authorization: `Bearer ${authToken}`,
            },
          });
          console.log("Estudante atualizado com sucesso.");
        } else {
        
          await api.post("/api/students", formData, {
            headers: {
              Authorization: `Bearer ${authToken}`,
            },
          });
          console.log("Estudante registrado com sucesso.");
        }
        getAllStudents();
        handleClose();
      } catch (error) {
        console.error("Erro ao salvar o estudante:", error);
      }
    }
  };

  return (
    <Modal show={show} onHide={handleClose} centered>
      <Modal.Header closeButton>
        <Modal.Title>
          {userToEdit ? "Editar Estudante" : "Cadastrar Estudante"}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form onSubmit={handleSubmit}>
          <div className="row">
            <div className="col-md-6 mb-3">
              <Form.Group>
                <Form.Control
                  type="text"
                  placeholder="Nome"
                  name="nome"
                  value={formData.nome}
                  onChange={handleChange}
                  isInvalid={errors.nome}
                  required
                />
                <Form.Control.Feedback type="invalid">
                  Nome é obrigatório.
                </Form.Control.Feedback>
              </Form.Group>
            </div>
            <div className="col-md-6 mb-3">
              <Form.Group>
                <Form.Control
                  type="text"
                  placeholder="Idade"
                  name="idade"
                  value={formData.idade}
                  onChange={handleChange}
                  isInvalid={errors.idade}
                  required
                />
                <Form.Control.Feedback type="invalid">
                  Idade é obrigatória.
                </Form.Control.Feedback>
              </Form.Group>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6 mb-3">
              <Form.Group>
                <Form.Control
                  type="text"
                  placeholder="Série"
                  name="serie"
                  value={formData.serie}
                  onChange={handleChange}
                  isInvalid={errors.serie}
                  required
                />
                <Form.Control.Feedback type="invalid">
                  Série é obrigatória.
                </Form.Control.Feedback>
              </Form.Group>
            </div>
            <div className="col-md-6 mb-3">
              <Form.Group>
                <Form.Control
                  type="text"
                  placeholder="Nota Média"
                  name="notaMedia"
                  value={formData.notaMedia}
                  onChange={handleChange}
                  isInvalid={errors.notaMedia}
                  required
                />
                <Form.Control.Feedback type="invalid">
                  Nota Média é obrigatória.
                </Form.Control.Feedback>
              </Form.Group>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6 mb-3">
              <Form.Group>
                <Form.Control
                  type="text"
                  placeholder="Endereço"
                  name="endereco"
                  value={formData.endereco}
                  onChange={handleChange}
                  isInvalid={errors.endereco}
                  required
                />
                <Form.Control.Feedback type="invalid">
                  Endereço é obrigatório.
                </Form.Control.Feedback>
              </Form.Group>
            </div>
            <div className="col-md-6 mb-3">
              <Form.Group>
                <Form.Control
                  type="text"
                  placeholder="Nome do Pai"
                  name="nomePai"
                  value={formData.nomePai}
                  onChange={handleChange}
                  isInvalid={errors.nomePai}
                  required
                />
                <Form.Control.Feedback type="invalid">
                  Nome do Pai é obrigatório.
                </Form.Control.Feedback>
              </Form.Group>
            </div>
          </div>
          <div className="row">
            <div className="col-md-6 mb-3">
              <Form.Group>
                <Form.Control
                  type="text"
                  placeholder="Nome da Mãe"
                  name="nomeMae"
                  value={formData.nomeMae}
                  onChange={handleChange}
                  isInvalid={errors.nomeMae}
                  required
                />
                <Form.Control.Feedback type="invalid">
                  Nome da Mãe é obrigatório.
                </Form.Control.Feedback>
              </Form.Group>
            </div>
            <div className="col-md-6 mb-3">
              <Form.Group>
                <Form.Control
                  type="date"
                  placeholder="Data de Nascimento"
                  name="dataNascimento"
                  value={formData.dataNascimento}
                  onChange={handleChange}
                  isInvalid={errors.dataNascimento}
                  required
                />
                <Form.Control.Feedback type="invalid">
                  Data de Nascimento é obrigatória.
                </Form.Control.Feedback>
              </Form.Group>
            </div>
          </div>
          <div className="text-end">
            <Button variant="primary" type="submit">
              {userToEdit ? "Salvar" : "Cadastrar"}
            </Button>
          </div>
        </Form>
      </Modal.Body>
    </Modal>
  );
}