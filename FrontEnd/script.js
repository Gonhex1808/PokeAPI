const form = document.querySelector("#form-tipo");
const inputPesquisa = document.querySelector("#pesquisa");
const mensagem = document.querySelector("#mensagem");
const listaPokemon = document.querySelector("#lista-pokemon");

// O backend usa esta porta, definida em BackEnd/Properties/launchSettings.json.
const API_URL = "http://localhost:5000/api/pokemon";

form.addEventListener("submit", async (event) => {
  event.preventDefault(); // Evita que a página seja recarregada.

  const pesquisa = inputPesquisa.value.trim().toLowerCase();
  listaPokemon.innerHTML = "";
  mensagem.textContent = "A procurar Pokémon...";

  try {
    // Primeiro interpreta a pesquisa como tipo: "fire", "water", etc.
    const respostaTipo = await fetch(`${API_URL}/type/${encodeURIComponent(pesquisa)}`);

    if (respostaTipo.ok) {
      const dados = await respostaTipo.json();
      mensagem.textContent = `${dados.count} Pokémon do tipo ${dados.type}:`;
      mostrarLista(dados.pokemons);
      return;
    }

    if (respostaTipo.status !== 404) {
      const erro = await respostaTipo.json().catch(() => null);
      mensagem.textContent = erro?.message || "Ocorreu um erro ao consultar a API.";
      return;
    }

    // Se não for um tipo, procura o mesmo texto como nome de Pokémon.
    const respostaPokemon = await fetch(`${API_URL}/name/${encodeURIComponent(pesquisa)}`);

    if (respostaPokemon.status === 404) {
      mensagem.textContent = "Não foi encontrado nenhum Pokémon ou tipo com esse nome. Verifica a escrita.";
      return;
    }

    if (!respostaPokemon.ok) {
      const erro = await respostaPokemon.json().catch(() => null);
      mensagem.textContent = erro?.message || "Ocorreu um erro ao consultar a API.";
      return;
    }

    const pokemon = await respostaPokemon.json();
    mensagem.textContent = "Pokémon encontrado:";
    mostrarLista([pokemon.name]);
  } catch (erro) {
    mensagem.textContent = "Não foi possível ligar à API.";
  }
});

async function carregarTodosPokemon() {
  mensagem.textContent = "A carregar todos os Pokémon...";

  try {
    const resposta = await fetch(`${API_URL}/all`);

    if (!resposta.ok) {
      throw new Error("Lista não encontrada");
    }

    const dados = await resposta.json();
    mensagem.textContent = `${dados.count} Pokémons carregados:`;
    mostrarLista(dados.pokemons);
  } catch (erro) {
    mensagem.textContent = "Não foi possível carregar a lista de Pokémon.";
  }
}

function mostrarLista(nomes) {
  nomes.forEach((nome) => {
    const item = document.createElement("li");
    const botao = document.createElement("button");
    botao.type = "button";
    botao.textContent = nome;
    botao.addEventListener("click", () => {
      window.location.href = `pokemon.html?name=${encodeURIComponent(nome)}`;
    });
    item.appendChild(botao);
    listaPokemon.appendChild(item);
  });
}

carregarTodosPokemon();
