<template>
  <h1>Project Page</h1>
  <p>{{msg}}</p>
    <ul>
        <table 
            v-for="item in items" 
            :key="item.id"
            :item="item"
            :index="index"
            >
            <tr>
                <td>{{item.id}}</td>
                <td>{{item.name}}</td>
            </tr>
        </table>

    </ul>
</template>

<script>
import Table from './Table.vue'
export default {
  components: { Table },
    name: 'ProjectPage',
    props:{
        msg: String,
        items: Array,
    }
}
</script>

<style>

</style>