use ToDo; 

CREATE TABLE Utilizador (
    Id INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    UtilizadorPassword NVARCHAR(255) NOT NULL,
    UtilizadorAdmin BIT NOT NULL DEFAULT 0,
    UltimoLogin DATETIME NULL,  -- Para verificar o tempo de ausencia
    DataCriacao DATETIME DEFAULT GETDATE() -- Data de criacao da conta
);

CREATE TABLE Categoria (
    Id INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100)
);

CREATE TABLE Tarefa (
    Id INT PRIMARY KEY IDENTITY,
    UtilizadorId INT FOREIGN KEY REFERENCES Utilizador(Id),
    Nome NVARCHAR(150) NOT NULL,
    Prioridade INT NOT NULL,  -- Prioridade: 1 (Ainda pode esperar), 2 (Importante), 3 (Muito Importante), 4 (Urgente)
    Estado NVARCHAR(50) DEFAULT 'Pendente', -- Estado: Pendente ou Concluida
    CategoriaId INT FOREIGN KEY REFERENCES Categoria(Id),
    DataCriacao DATETIME DEFAULT GETDATE(), -- Data da criacao da tarefa
    DataLimite DATETIME NULL    -- Data limite para a conclusao da tarefa 
);

CREATE TABLE Comentario (
    Id INT PRIMARY KEY IDENTITY,
    TarefaId INT FOREIGN KEY REFERENCES Tarefa(Id),
    Comentarios NVARCHAR(500) NULL,
    DataCriacao DATETIME DEFAULT GETDATE() -- Data de criacao do comentario
);
CREATE TABLE Historia (
    Id INT PRIMARY KEY IDENTITY,
    TarefaId INT FOREIGN KEY REFERENCES Tarefa(Id) ON DELETE SET NULL,
    DataConclusao DATETIME DEFAULT GETDATE() -- Data em que a tarefa foi marcada como concluida
);
CREATE TABLE Estatistica (
    Id INT PRIMARY KEY IDENTITY,
    UtilizadorId INT FOREIGN KEY REFERENCES Utilizador(Id) ON DELETE CASCADE,
    TarefaId INT FOREIGN KEY REFERENCES Tarefa(Id) ON DELETE CASCADE,
    DataAtualizacao DATETIME DEFAULT GETDATE() -- Data da ultima atualizacao
);



-- Tabela Utilizador
INSERT INTO Utilizador (Nome, Email, UtilizadorPassword, UtilizadorAdmin, UltimoLogin)
VALUES 
    ('admin', 'admin@example.com', '123', 1, NULL),  -- Conta de administrador
    ('joao_silva', 'joao.silva@example.com', 'senhaJoao123', 0, NULL),
    ('ana_oliveira', 'ana.oliveira@example.com', 'senhaAna123', 0, NULL),
    ('maria_fernandes', 'maria.fernandes@example.com', 'senhaMaria123', 0, NULL);

-- Tabela Categoria
INSERT INTO Categoria (Nome)
VALUES 
    ('Trabalho'),
    ('Pessoal'),
    ('Estudo'),
    ('Saude');

-- Tabela Tarefa
INSERT INTO Tarefa (UtilizadorId, Nome, Prioridade, Estado, CategoriaId, DataLimite)
VALUES 
    (2, 'Preparar apresentacao', 3, 'Pendente', 1, '2024-10-15'),
    (3, 'Comprar presentes de aniversario', 2, 'Pendente', 2, '2024-10-10'),
    (2, 'Revisar notas de estudo para exame', 3, 'Pendente', 3, '2024-10-20'),
    (4, 'Consulta medica', 4, 'Pendente', 4, '2024-10-12'),
    (3, 'Atualizar relatorio mensal', 2, 'Concluida', 1, NULL);

-- Tabela Comentario
INSERT INTO Comentario (TarefaId, Comentario)
VALUES 
    (1, 'Lembrar de adicionar slides sobre o orcamento.'),
    (2, 'Verificar se tem papel de presente.'),
    (3, 'Estudar mais sobre algebra linear.'),
    (4, 'Levar os exames anteriores.');

-- Tabela Historia
INSERT INTO Historia (TarefaId, DataConclusao)
VALUES 
    (5, '2024-10-01');  -- Tarefa concluida no dia 1 de outubro

-- Tabela Estatistica para uma tarefa especifica
INSERT INTO Estatistica (UtilizadorId, TarefaId, DataAtualizacao)
VALUES 
    (1, 1, GETDATE()), 
    (2, 3, GETDATE()); 


