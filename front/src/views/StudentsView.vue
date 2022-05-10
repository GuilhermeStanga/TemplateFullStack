<template>
  <v-data-table
    :headers="headers"
    :items="students"
    :footer-props="{ itemsPerPageOptions: [25,50,100,-1]}"
    :search="search"
    sort-by="calories"
    class="elevation-1"
  >
    <template v-slot:top>
      <v-toolbar
        flat
      >
        <v-text-field
          v-model="searchName"
          label="Buscar por nome"
          single-line
          hide-details
          
        ></v-text-field>
        <div class="input-group-append">
          <button
            class="btn btn-outline-secondary"
            type="button"
            @click="searchByName()"
          >
            Search
          </button>
        </div>

        <v-divider
          class="mx-6"
          inset
          vertical
        ></v-divider>
        <v-spacer></v-spacer>
        <v-dialog
          v-model="dialog"
          max-width="800px"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-btn
              color="primary"
              dark
              class="mb-2"
              v-bind="attrs"
              v-on="on"
            >
              Cadastrar aluno
            </v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="text-h5">{{ formTitle }}</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-row>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-text-field
                      v-model="editedItem.name"
                      label="Nome"
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-text-field
                      v-model="editedItem.cpf"
                      label="CPF"
                      :disabled=canEditing
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-text-field
                      v-model="editedItem.email"
                      label="E-mail"
                    ></v-text-field>
                  </v-col>
                  <v-col
                    cols="12"
                    sm="6"
                    md="4"
                  >
                    <v-text-field
                      v-model="editedItem.id"
                      label="RA"
                      disabled
                    ></v-text-field>
                  </v-col>

                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="blue darken-1"
                text
                @click="close"
              >
                Cancel
              </v-btn>
              <v-btn
                color="blue darken-1"
                text
                @click="save"
              >
                Save
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">Deseja deletar este aluno?</v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="closeDelete">Cancelar</v-btn>
              <v-btn color="blue darken-1" text @click="deleteItemConfirm">Sim</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:[`item.actions`]='{ item }'>
      <v-icon
        small
        class="mr-2"
        @click="editItem(item)"
      >
        mdi-pencil
      </v-icon>
      <v-icon
        small
        @click="deleteItem(item)"
      >
        mdi-delete
      </v-icon>
    </template>
    <template v-slot:no-data>
      <v-btn
        color="primary"
        @click="initialize"
      >
        Reset
      </v-btn>
    </template>
  </v-data-table>
</template>

<script lang="ts">
  import Vue from 'vue'
  import DataService from "../services/DataService";

  var indexUpdate = -1;
  var itemUpdate = {};

  export default Vue.extend({

    data: () => ({
      searchName: "",
      footerProps: {'items-per-page-options': [15, 30, 50, 100]},
      dialog: false,
      dialogDelete: false,
      headers: [
        {
          text: 'RA',
          align: 'start',
          sortable: false,
          value: 'id',
        },
        { text: 'Nome', value: 'name' },
        { text: 'CPF', value: 'cpf' },
        { text: 'E-mail', value: 'email' },
        { text: 'Actions', value: 'actions', sortable: false }
      ],
      students: [],
      editedIndex: -1,
      editedItem: {
        name: '',
        cpf: 0,
        email: 0,
        id: 0,
      },
      defaultItem: {
        name: '',
        cpf: 0,
        email: 0,
        id: 0,
      },
    }),

    computed: {
      formTitle () {
        return this.editedIndex === -1 ? 'Cadastrar aluno' : 'Editar aluno'
      },
      canEditing () {
        return this.editedIndex === -1 ? false : true
      }
    },

    watch: {
      dialog (val) {
        val || this.close()
      },
      dialogDelete (val) {
        val || this.closeDelete()
      },
    },

    created () {
      this.initialize()
    },

    methods: {
      initialize () {
        DataService.getAll("")
          .then((response) => {
            console.log(response);
            this.students = response.data
          })
          .catch((e) => {
            console.log(e);
          });
      },

      searchByName() {
        DataService.getAll(this.searchName)
          .then((response) => {
            console.log(response);
            this.students = response.data
          })
          .catch((e) => {
            console.log(e);
          });
      },

      editItem (item) {
        this.editedIndex = this.students.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialog = true
      },

      deleteItem (item) {
        this.editedIndex = this.students.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.dialogDelete = true
      },

      deleteItemConfirm () {
        DataService.delete(this.editedItem.id)
          .then((response) => {
            console.log(response);
            this.students.splice(this.editedIndex, 1)
            this.closeDelete()
            this.$toast.info('Aluno deletado com sucesso');
          })
          .catch((e) => {
            console.log(e);
          });
      },

      close () {
        this.dialog = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },

      closeDelete () {
        this.dialogDelete = false
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem)
          this.editedIndex = -1
        })
      },

      save () {
        if (this.editedIndex > -1) {
          indexUpdate = this.editedIndex;
          itemUpdate = this.editedItem;
          DataService.update(this.editedItem.id, this.editedItem)
          .then((response) => {
            console.log(response);
            Object.assign(this.students[indexUpdate], itemUpdate)
            this.$toast.info('Aluno alterado com sucesso');
          })
          .catch((e) => {
            console.log(e);
            var msg = e.response.data.errors['Invalid_data'][0];
            this.$toast.error('Erro! ' + this.parseErrorMessage(msg));
          });
        } else {
          DataService.create(this.editedItem)
          .then((response) => {
            console.log(response);
            this.editedItem.id = response.data.id;
            this.students.push(response.data)
            this.$toast.info('Aluno cadastrado com sucesso');
          })
          .catch((e) => {
            console.log(e);
            var msg = e.response.data.errors['Invalid_data'][0];
            this.$toast.error('Erro! ' + this.parseErrorMessage(msg));
          });
        }
        this.close()
      },

      parseErrorMessage(error){
        switch(error){
          case 'INVALID_CPF':
            return 'CPF inválido';
          case 'INVALID_EMAIL':
            return 'E-mail inválido';
          case 'CPF_ALREADY_REGISTERED':
            return 'CPF já cadastrado'
        }
      }

    },
  })
</script>
